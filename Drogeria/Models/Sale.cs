using System.ComponentModel.DataAnnotations.Schema;

namespace Drogeria.Models;
public class Sale
{
    public int SaleId { get; set; }

    public int? CustomerId { get; set; }
    public Customer? Customer { get; set; }

    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;

    public DateTime SaleDate { get; set; } = DateTime.Now;

    public PaymentMethod PaymentMethod { get; set; }

    [Column(TypeName = "decimal(12,2)")]
    public decimal TotalNet { get; set; }

    [Column(TypeName = "decimal(12,2)")]
    public decimal TotalGross { get; set; }

    public ICollection<SaleItem>? Items { get; set; }
}