using System.ComponentModel.DataAnnotations;

namespace OrderService.DTOs
{
    public class CreateOrderDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        [MinLength(1)]
        public List<OrderItemDto> Items { get; set; } = new();
    }
}