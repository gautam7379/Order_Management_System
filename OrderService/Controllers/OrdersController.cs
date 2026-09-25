using Microsoft.AspNetCore.Mvc;
using OrderService.DTOs;
using OrderService.Services;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(
            [FromBody] CreateOrderDto request)
        {
            try
            {
                var order =
                    await _orderService.CreateOrderAsync(request);

                return CreatedAtAction(
                    nameof(GetOrderById),
                    new { id = order.OrderId },
                    order);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (HttpRequestException)
            {
                return StatusCode(
                    503,
                    new
                    {
                        message = "Dependent service is unavailable."
                    });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders =
                await _orderService.GetAllOrdersAsync();

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order =
                await _orderService.GetOrderByIdAsync(id);

            if (order == null)
                return NotFound(new { message = "Order not found." });

            return Ok(order);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(
            int id,
            [FromBody] UpdateOrderStatusDto request)
        {
            var updated =
                await _orderService.UpdateStatusAsync(
                    id,
                    request.Status);

            if (!updated)
                return NotFound(new { message = "Order not found." });

            return Ok(new
            {
                message = "Order status updated successfully."
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            var cancelled =
                await _orderService.CancelOrderAsync(id);

            if (!cancelled)
                return NotFound(new { message = "Order not found." });

            return Ok(new
            {
                message = "Order cancelled successfully."
            });
        }
    }
}