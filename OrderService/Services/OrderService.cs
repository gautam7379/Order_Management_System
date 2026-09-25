using OrderService.DTOs;
using OrderService.Models;
using OrderService.Repositories;

namespace OrderService.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Order> CreateOrderAsync(CreateOrderDto request)
        {
            if (request == null)
                throw new ArgumentException("Order request is required.");

            if (request.Items == null || request.Items.Count == 0)
                throw new ArgumentException(
                    "Order must contain at least one item.");

            var order = new Order
            {
                CustomerId = request.CustomerId,
                OrderDate = DateTime.UtcNow,
                Status = "Pending",
                TotalAmount = 0
            };

            decimal total = 0;

            foreach (var item in request.Items)
            {
                if (item.Quantity <= 0)
                    throw new ArgumentException(
                        "Quantity must be greater than zero.");

                var orderItem = new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = 0
                };

                order.Items.Add(orderItem);

                // Product Service is not used.
                // Therefore, product price is not available here.
                total += 0;
            }

            order.TotalAmount = total;

            return await _repository.CreateAsync(order);
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateStatusAsync(int id, string status)
        {
            var order = await _repository.GetByIdAsync(id);

            if (order == null)
                return false;

            order.Status = status;

            return await _repository.UpdateAsync(order);
        }

        public async Task<bool> CancelOrderAsync(int id)
        {
            var order = await _repository.GetByIdAsync(id);

            if (order == null)
                return false;

            order.Status = "Cancelled";

            return await _repository.UpdateAsync(order);
        }
    }
}