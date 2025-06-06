using System.ComponentModel.DataAnnotations.Schema;

namespace Drogeria.Models;

public class PurchaseOrder
{
    public int PurchaseOrderId { get; set; }

    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; } = null!;

    public DateTime OrderDate { get; set; } = DateTime.Now;
    public DateTime? ExpectedDelivery { get; set; }

    public PurchaseOrderStatus Status { get; set; } = PurchaseOrderStatus.Draft;

    [Column(TypeName = "decimal(12,2)")]
    public decimal TotalNet { get; set; }

    [Column(TypeName = "decimal(12,2)")]
    public decimal TotalGross { get; set; }

    public ICollection<PurchaseOrderLine>? Lines { get; set; }
}