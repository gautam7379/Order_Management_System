# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project file
COPY ["Product Service.csproj", "."]

# Restore dependencies
RUN dotnet restore "Product Service.csproj"

# Copy the remaining source code
COPY . .

# Build the application
RUN dotnet build "Product Service.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "Product Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Copy published application
COPY --from=publish /app/publish .

# Expose application port
EXPOSE 8080

# Start the application
ENTRYPOINT ["dotnet", "Product Service.dll"]