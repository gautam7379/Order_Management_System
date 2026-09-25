using OrderService.DTOs;
using OrderService.Models;

namespace OrderService.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(CreateOrderDto request);

        Task<List<Order>> GetAllOrdersAsync();

        Task<Order?> GetOrderByIdAsync(int id);

        Task<bool> UpdateStatusAsync(int id, string status);

        Task<bool> CancelOrderAsync(int id);
    }
}