using AGInventoryManagement.Domain.Common;

namespace AGInventoryManagement.Domain.Stocks;

public class Stock : BaseAuditableEntity
{
    private Stock(
        Guid id,
        Guid productId,
        int quantityOnHand,
        int idealQuantity)
        : base(id)
    {
        ProductId = productId;
        QuantityOnHand = quantityOnHand;
        IdealQuantity = idealQuantity;
    }

    public Guid ProductId { get; private set; }

    public int QuantityOnHand { get; private set; }

    public int IdealQuantity { get; private set; }


    private readonly List<StockSnapshot> _snapshots = [];

    public IReadOnlyList<StockSnapshot> Snapshots => [.. _snapshots];

    public static DomainResult<Stock> Create(Guid productId)
    {
        var stock = new Stock(
            Guid.NewGuid(),
            productId,
            0,
            0);

        return stock;
    }

    public DomainResult UpdateQuantityOnHand(int adjustment)
    {
        QuantityOnHand += adjustment;

        if (QuantityOnHand < 0)
        {
            return DomainResult.Failure<Stock>(StockErrors.Negative);
        }

        var snapshot = new StockSnapshot(
            Guid.NewGuid(),
            Id,
            ProductId,
            DateTime.Now,
            QuantityOnHand);

        _snapshots.Add(snapshot);

        return DomainResult.Success();
    }

    public DomainResult UpdateIdealQuantity(int adjustment)
    {
        IdealQuantity += adjustment;

        if (IdealQuantity < 0)
        {
            return DomainResult.Failure<Stock>(StockErrors.Negative);
        }

        return DomainResult.Success();
    }

    private Stock() { } // EF
}
