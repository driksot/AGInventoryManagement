namespace AGInventoryManagement.Domain.Stocks;

public record StockSnapshot(
    Guid Id,
    Guid StockId,
    Guid ProductId,
    DateTime SnapshotTime,
    int Quantity);
