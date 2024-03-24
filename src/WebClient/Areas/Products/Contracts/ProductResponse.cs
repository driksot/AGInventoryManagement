namespace AGInventoryManagement.WebClient.Areas.Products.Contracts;

public record ProductResponse(
    Guid Id,
    string Name,
    string? Description,
    decimal Price,
    string Sku,
    int StockOnHand,
    int StockIdeal);
