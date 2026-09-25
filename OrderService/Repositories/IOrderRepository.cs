using OrderService.Models;

namespace OrderService.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> CreateAsync(Order order);

        Task<List<Order>> GetAllAsync();

        Task<Order?> GetByIdAsync(int id);

        Task<bool> UpdateAsync(Order order);

        Task<bool> DeleteAsync(Order order);
    }
}