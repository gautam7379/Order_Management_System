using System.ComponentModel.DataAnnotations;

namespace CustomerService.Models;

public class Customer
{
    public int CustomerId { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Phone { get; set; } = string.Empty;

    [Required]
    public string City { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }

    public bool IsActive { get; set; }
} 