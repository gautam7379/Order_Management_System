using ProductService.Models;

namespace ProductService.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product?> GetByIdAsync(int id);

        Task<Product> AddAsync(Product product);

        Task<Product?> UpdateAsync(Product product);

        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<Product>> GetByCategoryAsync(string category);

        Task<Product?> UpdateStockAsync(int id, int quantity);
    }
}