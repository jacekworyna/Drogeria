using System.ComponentModel.DataAnnotations;

namespace Drogeria.Models;

public class Supplier
{
    public int SupplierId { get; set; }

    [Required, MaxLength(150)]
    public string Name { get; set; } = null!;

    [MaxLength(250)]
    public string? Address { get; set; }

    [MaxLength(50)]
    public string? Phone { get; set; }

    [MaxLength(150)]
    public string? Email { get; set; }
    
    public bool IsActive { get; set; } = true; 

    public ICollection<PurchaseOrder>? PurchaseOrders { get; set; }
}