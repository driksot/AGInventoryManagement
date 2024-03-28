using AGInventoryManagement.Domain.Common;
using AGInventoryManagement.Domain.Stocks;

namespace AGInventoryManagement.Domain.Products;

public class Product : BaseAuditableEntity, ISoftDeletable
{
    private const string _agPrefix = "AG";

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

    public static DomainResult<Product> Create(string name, string? description, decimal price, int productCount)
    {
        var productId = Guid.NewGuid();
        var sku = Sku.Create(GenerateSku(name, productCount + 1));

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

    private static string GenerateSku(string name, int productCount)
    {
        // Format of sku = AG-GEM-00000
        string sequenceFormat = "00000.##";

        var shortName = name[..3].ToUpper();

        var sequenceString = productCount.ToString(sequenceFormat);

        var sku = string.Join("-", _agPrefix, shortName, sequenceString);

        return sku;
    }

#pragma warning disable CS8618
    private Product() { } // EF
#pragma warning restore CS8618
}
