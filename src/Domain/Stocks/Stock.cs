using AGInventoryManagement.Domain.Common;

namespace AGInventoryManagement.Domain.Stocks;

public class Stock : BaseAuditableEntity
{
    public Guid ProductId { get; set; }

    public int QuantityOnHand { get; set; }

    public int IdealQuantity { get; set; }

    public IList<StockSnapshot> Snapshots { get; set; } = [];
}
