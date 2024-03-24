using AGInventoryManagement.Domain.Common;
using AGInventoryManagement.Domain.Stocks;

namespace AGInventoryManagement.Domain.Products;

public class Product : BaseAuditableEntity, ISoftDeletable
{
    private const string _agPrefix = "AG-";

    private Product(
        Guid id,
        string name,
        string? description,
        decimal price,
        Sku sku,
        Stock stock) 
        : base(id)
    {
        Name = name;
        Description = description;
        Price = price;
        Sku = sku;
        Stock = stock;
    }

    public string Name { get; private set; }

    public string? Description { get; private set; }

    public decimal Price { get; private set; }

    public Sku Sku { get; private set; }

    public Stock Stock { get; private set; }

    public bool IsDeleted { get; set; } = false;

    public DateTime? DeletedOnUtc { get; set; }

    public static DomainResult<Product> Create(string name, string? description, decimal price)
    {
        var productId = Guid.NewGuid();
        var sku = Sku.Create(GenerateSku(name, productId));

        if (sku is null)
        {
            return DomainResult.Failure<Product>(ProductErrors.InvalidSku);
        }

        var product = new Product(
            productId,
            name,
            description,
            price,
            sku,
            Stock.Create(productId).Value);

        if (product.Stock is null)
        {
            return DomainResult.Failure<Product>(ProductErrors.MissingStock);
        }

        return product;
    }

    public DomainResult Update(
        string name,
        string description,
        decimal price)
    {
        Name = name;
        Description = description; 
        Price = price;

        return DomainResult.Success();
    }

    private static string GenerateSku(string name, Guid productId)
    {
        var sku = _agPrefix
            + name[..3].ToUpper()
            + productId.ToString().ToUpper()[(productId.ToString().Length - 6)..];

        return sku;
    }

#pragma warning disable CS8618
    private Product() { } // EF
#pragma warning restore CS8618
}
