using System.ComponentModel.DataAnnotations;

namespace Drogeria.Models;
public class StockLevel
{
    [Key]
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public int QtyOnHand { get; set; }
    public int ReorderLevel { get; set; } = 5;
}