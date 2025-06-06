using System.ComponentModel.DataAnnotations;

namespace Drogeria.Models;

public class Customer
{
    public int CustomerId { get; set; }

    [Required, MaxLength(100)]
    public string FirstName { get; set; } = null!;

    [Required, MaxLength(100)]
    public string LastName { get; set; } = null!;

    [MaxLength(50)]
    public string? Phone { get; set; }

    [MaxLength(150)]
    public string? Email { get; set; }

    [MaxLength(30)]
    public string? LoyaltyCardNo { get; set; }

    public int Points { get; set; }
    
    public bool IsActive { get; set; } = true; 

    public ICollection<Sale>? Sales { get; set; }
}