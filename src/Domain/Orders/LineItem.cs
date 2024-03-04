using AGInventoryManagement.Domain.Common;

namespace AGInventoryManagement.Domain.Orders;

public class LineItem : BaseEntity
{
    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    public decimal UnitPrice { get; set; }
}
