using ProductService.Models;

namespace ProductService.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<Product?> GetProductByIdAsync(int id);

        Task<Product> CreateProductAsync(Product product);

        Task<Product?> UpdateProductAsync(int id, Product product);

        Task<bool> DeleteProductAsync(int id);

        Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category);

        Task<Product?> UpdateStockAsync(int id, int quantity);
    }
}