using System.ComponentModel.DataAnnotations.Schema;

namespace Drogeria.Models;
public class SaleItem
{
    public int SaleItemId { get; set; }

    public int SaleId { get; set; }
    public Sale Sale { get; set; } = null!;

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal UnitPrice { get; set; }

    [Column(TypeName = "decimal(4,2)")]
    public decimal VatRate { get; set; }

    [NotMapped]
    public decimal LineNet => UnitPrice * Quantity;

    [NotMapped]
    public decimal LineGross => LineNet * (1 + VatRate);
}