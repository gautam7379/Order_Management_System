using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Models;

namespace ProductService.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<Product> AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product?> UpdateAsync(Product product)
        {
            var existingProduct = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == product.ProductId);

            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.AvailableQuantity = product.AvailableQuantity;
            existingProduct.Category = product.Category;
            existingProduct.IsActive = product.IsActive;

            await _context.SaveChangesAsync();

            return existingProduct;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return false;
            }

            product.IsActive = false;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(string category)
        {
            return await _context.Products
                .Where(p => p.Category == category)
                .ToListAsync();
        }

        public async Task<Product?> UpdateStockAsync(int id, int quantity)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return null;
            }

            product.AvailableQuantity = quantity;

            await _context.SaveChangesAsync();

            return product;
        }
    }
}