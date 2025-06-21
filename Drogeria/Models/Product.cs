using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Drogeria.Models;

public class Product
{
    public int ProductId { get; set; }

    [Required, MaxLength(150)]
    public string Name { get; set; } = null!;

    [Required, MaxLength(100)]
    public string Brand { get; set; } = null!;

    [MaxLength(50)]
    public string? Size { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

    [Column(TypeName = "decimal(4,2)")]
    public decimal VatRate { get; set; } = 0.23m;

    [MaxLength(20)]
    public string? EAN { get; set; }

    public bool IsActive { get; set; } = true;

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; } = null!;

    public ICollection<SaleItem>? SaleItems { get; set; }
    public ICollection<PurchaseOrderLine>? PurchaseOrderLines { get; set; }
    public ICollection<InventoryMovement>? InventoryMovements { get; set; }
    public StockLevel? StockLevel { get; set; }
}