using AGInventoryManagement.Domain.Common;
using AGInventoryManagement.Domain.Stocks;

namespace AGInventoryManagement.Domain.Products;

public class Product : BaseAuditableEntity
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public Sku Sku { get; set; } = null!;

    public Stock Stock { get; set; } = null!;

    public bool IsArchived { get; set; } = false;
}
