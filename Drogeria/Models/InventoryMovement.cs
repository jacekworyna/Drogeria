namespace Drogeria.Models;

public class InventoryMovement
{
    public int InventoryMovementId { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public MovementType MovementType { get; set; }

    public int QuantityChange { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.Now;

    public int? SourceLineId { get; set; }
    public string? SourceTable { get; set; }
}