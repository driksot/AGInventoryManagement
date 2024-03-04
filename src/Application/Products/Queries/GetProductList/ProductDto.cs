namespace AGInventoryManagement.Application.Products.Queries.GetProductList;

public class ProductDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string Sku { get; set; } = null!;

    public int StockOnHand { get; set; }

    public int StockIdeal { get; set; }
}
