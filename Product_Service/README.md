# Product Service

## 1. Project Overview

Product Service is an ASP.NET Core Web API microservice developed for the Order Management System.

The service is responsible for managing product information and inventory-related operations.

The service provides:

- Product creation
- Product retrieval
- Product update
- Product deletion/deactivation
- Product filtering by category
- Stock quantity update
- Input validation
- SQL Server persistence
- Swagger API documentation
- Docker containerization
- Kubernetes deployment

### Architecture

The service follows a layered architecture:

```text
Controller
    ↓
Service
    ↓
Repository
    ↓
Entity Framework Core
    ↓
SQL Server
```

## 2. Technology Stack

- C#
- ASP.NET Core Web API
- .NET 10
- Entity Framework Core
- Microsoft SQL Server
- Swagger / OpenAPI
- Docker
- Kubernetes
- Git / GitHub

## 3. Project Structure

```text
Product Service
│
├── Controllers
│   └── ProductsController.cs
│
├── Data
│   └── ProductDbContext.cs
│
├── Models
│   └── Product.cs
│
├── Repositories
│   ├── IProductRepository.cs
│   └── ProductRepository.cs
│
├── Services
│   ├── IProductService.cs
│   └── ProductService.cs
│
├── Migrations
│
├── k8s
│   ├── product-service-deployment.yaml
│   └── product-service-service.yaml
│
├── Dockerfile
├── Program.cs
├── appsettings.json
├── .gitignore
├── README.md
└── Product Service.csproj
```

## 4. Prerequisites

Install the following before running the project:

- .NET 10 SDK
- Visual Studio
- Microsoft SQL Server
- SQL Server Management Studio (SSMS) or another SQL client
- Docker Desktop
- Kubernetes enabled in Docker Desktop
- kubectl
- Git

### NuGet Packages

The project uses the following NuGet packages:

| Package | Version | Purpose |
|---|---|---|
| Microsoft.AspNetCore.OpenApi | 10.0.11 | OpenAPI support for the ASP.NET Core Web API |
| Microsoft.EntityFrameworkCore.SqlServer | 10.0.12 | Entity Framework Core provider for Microsoft SQL Server |
| Microsoft.EntityFrameworkCore.Tools | 10.0.12 | Entity Framework Core migration and database management tools |
| Swashbuckle.AspNetCore | 10.2.3 | Swagger/OpenAPI documentation and Swagger UI |

## 5. Database Setup

The application uses Microsoft SQL Server with Entity Framework Core.

### Database

**Database name:** `ProductDB`

**Main table:** `Products`

**Product fields:**
- ProductId
- Name
- Description
- Price
- AvailableQuantity
- Category
- IsActive

### Database Migration

The database schema is created using Entity Framework Core migrations.

Run the following from the Visual Studio Package Manager Console:

```powershell
Add-Migration InitialCreate
Update-Database
```

If a different connection string is required for the target SQL Server, provide the appropriate connection string through the EF Core migration command.

### Database Security

Database usernames and passwords must not be committed to GitHub.

Configure the connection string through environment-specific configuration or environment variables.

```text
Add-Migration InitialCreate
        ↓
Update-Database
        ↓
ProductDB
        ↓
Products table
```

### 5.1 Setup Instructions

Follow these steps to set up and run the Product Service locally.

#### 5.1.1 Clone the Repository

```bash
git clone <YOUR_GITHUB_REPOSITORY_URL>
cd <YOUR_PROJECT_FOLDER>
```

#### 5.1.2 Restore Dependencies

```bash
dotnet restore
```

#### 5.1.3 Configure the Database Connection

Configure the SQL Server connection string as described in the **Configuration** section below.

Do not commit actual passwords or other secrets to GitHub.

#### 5.1.4 Apply Database Migration

From the Visual Studio Package Manager Console, run:

```powershell
Update-Database
```

#### 5.1.5 Build the Project

```bash
dotnet build
```

#### 5.1.6 Run the Application

```bash
dotnet run
```

#### 5.1.7 Verify the Application

Open Swagger UI using the configured application URL:

```text
/swagger
```

For example:

```text
http://localhost:5052/swagger
```

Use Swagger to verify:

* Product CRUD operations
* Request validation
* Category filtering
* Stock update functionality

## 6. Configuration

The application uses the following configuration key:

```text
ConnectionStrings__DefaultConnection
```

Example:

```text
Server=<SQL_SERVER>;Database=ProductDB;User Id=<USERNAME>;Password=<PASSWORD>;TrustServerCertificate=True;
```

Replace the placeholder values with the appropriate SQL Server details for your environment.

**Security:** Do not commit actual database passwords, connection strings containing credentials, or other secrets to the repository.


## 7. Build Instructions

Restore the project dependencies:

```bash
dotnet restore
```

Build the project:

```bash
dotnet build
dotnet publish -c Release
```

The project should build successfully without compilation errors.

## 8. Run Instructions

Run the application using:

```bash
dotnet run
```

The API can then be accessed through the configured application URL.

Swagger UI:

```text
/swagger
```

For example:

```text
http://localhost:5052/swagger
```

## 9. Testing Instructions

The API can be tested using Swagger UI.

The following operations should be verified:

- Create product
- Get all products
- Get product by ID
- Update product
- Delete/deactivate product
- Filter products by category
- Update stock

Validation should also be tested for:

- Empty product name
- Negative price
- Negative available quantity
- Invalid product ID
- Negative stock quantity

## 10. API Endpoint Summary

Base route:

```text
/api/products
```

| Method | Endpoint | Description |
|---|---|---|
| POST | /api/products | Add a new product |
| GET | /api/products | Get all products |
| GET | /api/products/{id} | Get product by ID |
| PUT | /api/products/{id} | Update a product |
| DELETE | /api/products/{id} | Delete/deactivate a product |
| GET | /api/products/category?category={category} | Filter products by category |
| PUT | /api/products/{id}/stock | Update product stock |

### HTTP Status Codes

The API uses appropriate HTTP status codes including:

- 200 OK
- 201 Created
- 204 No Content
- 400 Bad Request
- 404 Not Found

## 11. Docker Instructions

The Product Service is containerized using Docker.

Each service should have its own Dockerfile. The Product Service contains a Dockerfile in the project root.

### 11.1 Purpose of the Dockerfile

The Dockerfile is used to:

- Build the ASP.NET Core application.
- Restore project dependencies.
- Publish the application.
- Create a runtime Docker image.
- Expose the application on port 8080.
- Start the Product Service inside the container.

The Dockerfile uses separate build, publish and runtime stages to create the application image.

### 11.2 Docker Image vs Docker Container

A Docker image is a packaged, read-only template containing the application and its required runtime components.

A Docker container is a running instance of a Docker image.

For this project:

```text
Product Service Source Code
        ↓
Dockerfile
        ↓
Docker Image
        ↓
Docker Container
        ↓
Product Service API
```

### 11.3 Build the Docker Image

From the Product Service project directory, build the Docker image:

```bash
docker build -t product-service:latest .
```

The command:

- Reads the Dockerfile.
- Restores the .NET project dependencies.
- Builds the application.
- Publishes the application.
- Creates the final runtime image.
- Tags the image as `product-service:latest`.

Verify the image:

```bash
docker images
```

The image should appear as:

```text
product-service   latest
```

### 11.4 Run the Docker Container

Run the Product Service container:

```bash
docker run -d --name product-service-container -p 8080:8080 -e ASPNETCORE_ENVIRONMENT=Development product-service:latest
```

The port mapping is:

```text
Host Port 8080
      ↓
Container Port 8080
```

The `ASPNETCORE_ENVIRONMENT=Development` setting enables Swagger UI.

### 11.5 Check Running Containers

Verify that the container is running:

```bash
docker ps
```

The Product Service container should appear with port mapping similar to:

```text
0.0.0.0:8080->8080/tcp
```

### 11.6 View Container Logs

View the application logs:

```bash
docker logs product-service-container
```

The logs can be used to verify application startup and diagnose runtime problems.

### 11.7 Access the API

The API remains accessible while running inside the Docker container.

API endpoint:

```text
http://localhost:8080/api/products
```

Swagger UI:

```text
http://localhost:8080/swagger/index.html
```

### 11.8 Docker Port

The ASP.NET Core application listens on port:

```text
8080
```

The Dockerfile exposes:

```dockerfile
EXPOSE 8080
```

Docker maps the host port 8080 to the container port 8080.

### 11.9 Application Configuration

The application reads the database connection string using:

```text
ConnectionStrings__DefaultConnection
```

Sensitive information such as database passwords must not be committed to GitHub.

For different environments, the database connection should be supplied through environment-specific configuration or environment variables.

### 11.10 Database Connectivity

When the Product Service runs inside Docker, the application must be able to connect to the configured SQL Server database.

The connection configuration used for the container must point to a SQL Server instance that is reachable from the Docker container.

The database used by this service is:

```text
ProductDB
```

### 11.11 Basic Docker Troubleshooting

If the container does not start:

```bash
docker ps -a
```

Check the container logs:

```bash
docker logs product-service-container
```

Check the Docker image:

```bash
docker images
```

If the container name already exists, remove the old container:

```bash
docker rm -f product-service-container
```

Then run the container again.

### 11.12 Docker Verification

The following Docker operations were verified for the Product Service:

- `docker build`
- `docker run`
- `docker ps`
- `docker logs`

The API was successfully accessed while running inside the Docker container through Swagger and the Product API endpoint.

## 12. Kubernetes Instructions

The Product Service includes Kubernetes configuration files in the `k8s` folder.

The Kubernetes configuration contains:

- Deployment YAML
- Service YAML

The Kubernetes setup was tested using Docker Desktop Kubernetes.

### 12.1 Kubernetes Deployment

The Deployment configuration is stored in:

```text
k8s/product-service-deployment.yaml
```

The Deployment creates and manages the Product Service Pod.

Apply the Deployment:

```bash
kubectl apply -f k8s/product-service-deployment.yaml
```

Expected result:

```text
deployment.apps/product-service created
```

### 12.2 Check Kubernetes Deployment

Check the deployed application:

```bash
kubectl get deployments
```

The Product Service Deployment should show:

```text
product-service
```

### 12.3 Check Kubernetes Pods

Check the running Pods:

```bash
kubectl get pods
```

The Product Service Pod should show:

```text
1/1   Running
```

### 12.4 Kubernetes Service

The Service configuration is stored in:

```text
k8s/product-service-service.yaml
```

The Kubernetes Service exposes the Product Service application.

Apply the Service:

```bash
kubectl apply -f k8s/product-service-service.yaml
```

Expected result:

```text
service/product-service created
```

### 12.5 Check Kubernetes Services

Check the available Kubernetes Services:

```bash
kubectl get services
```

The Product Service is configured as a NodePort service.

Example:

```text
product-service   NodePort   8080:30080/TCP
```

### 12.6 View Kubernetes Pod Logs

To view the Product Service logs:

```bash
kubectl logs <product-service-pod-name>
```

The logs can be used to verify application startup and troubleshoot runtime problems.

Example startup information:

```text
Now listening on: http://[::]:8080
Application started.
Hosting environment: Development
```

### 12.7 Verify Kubernetes Service Endpoints

The Service endpoint can be checked using:

```bash
kubectl get endpoints product-service
```

The endpoint should point to the Product Service Pod on port 8080.

### 12.8 Test the Service from Inside Kubernetes

The Product Service can be tested from inside the Kubernetes cluster using a temporary test Pod:

```bash
kubectl run test-curl --rm -it --image=curlimages/curl --restart=Never -- curl http://product-service:8080/api/products
```

A successful response returns the products from the ProductDB database.

### 12.9 Local Kubernetes Verification

For local verification, the Kubernetes Service can be accessed using `kubectl port-forward`.

Run:

```bash
kubectl port-forward service/product-service 8096:8080
```

The mapping is:

```text
localhost:8096
      ↓
Kubernetes Service:8080
      ↓
Product Service Pod:8080
```

### 12.10 Access the Kubernetes API

After port forwarding, the Product API can be accessed at:

```text
http://localhost:8096/api/products
```

### 12.11 Access Swagger in Kubernetes

Swagger UI can be accessed at:

```text
http://localhost:8096/swagger/index.html
```

The Kubernetes Deployment sets the ASP.NET Core environment to Development so that Swagger UI is enabled.

### 12.12 Kubernetes Verification

The following Kubernetes operations were verified for the Product Service:

```bash
kubectl apply -f product-service-deployment.yaml
kubectl apply -f product-service-service.yaml
kubectl get pods
kubectl get deployments
kubectl get services
kubectl logs <product-service-pod-name>
```

The Product Service was successfully deployed to the Docker Desktop Kubernetes cluster and the API was verified through the Kubernetes Service.

```text
Deployment YAML
      ↓
Kubernetes Deployment
      ↓
Pod
      ↓
Service YAML
      ↓
Kubernetes Service
      ↓
Port Forward
      ↓
Product API / Swagger
```

## 13. Validation and Error Handling

The API validates product input.

**Product Name**
Product name is required.

**Price**
Price must be greater than or equal to zero.

**Available Quantity**
Available quantity must be greater than or equal to zero.

**Product ID**
Requests for unknown product IDs return:

```text
404 Not Found
```

**Stock**
Stock quantity cannot be negative.

Invalid requests return an appropriate HTTP response instead of causing an unhandled exception.

## 14. Delete / Deactivation Behavior

The DELETE operation deactivates the product by setting:

```text
IsActive = false
```

The product record is therefore retained in the database instead of being physically removed.

## 15. Known Limitations / Assumptions

- The service currently uses SQL Server for persistence.
- Database connection configuration must be supplied for the target environment.
- Secrets such as database passwords must be provided through secure configuration and must not be committed to GitHub.
- Kubernetes configuration is intended for the local Docker Desktop Kubernetes environment used during development.
- Category filtering is exposed as `/api/products/category?category={category}` to provide a separate Swagger operation for category filtering.
- Authentication and authorization are not implemented in this Product Service.
- The service is currently independently deployable and can be integrated with other services through the team's API Gateway.

## 16. Future Integration

The Product Service will be integrated with the other services in the Order Management System through the API Gateway.

The final system is expected to contain multiple independently deployable services that communicate through the defined APIs.