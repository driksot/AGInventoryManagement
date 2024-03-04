using AGInventoryManagement.Application.Products.Queries.GetProductList;

namespace AGInventoryManagement.Application.Stocks.Queries.GetSnapshotList;

public class SnapshotDto
{
    public Guid Id { get; set; }

    public ProductDto Product { get; set; } = new ProductDto();

    public DateTime SnapshotTime { get; set; }

    public int Quantity { get; set; }
}
