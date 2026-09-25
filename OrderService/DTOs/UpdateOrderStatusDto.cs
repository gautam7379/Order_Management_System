using System.ComponentModel.DataAnnotations;

namespace OrderService.DTOs
{
    public class UpdateOrderStatusDto
    {
        [Required]
        public string Status { get; set; } = string.Empty;
    }
}