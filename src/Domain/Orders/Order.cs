using AGInventoryManagement.Domain.Common;
using AGInventoryManagement.Domain.Shared;

namespace AGInventoryManagement.Domain.Orders;

public class Order : BaseAuditableEntity
{
    public string OrderNumber { get; set; } = null!;

    public Guid CustomerId { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Generated;

    public DateTime OrderDate { get; set; }

    public Address? OrderAddress { get; set; }

    public IList<LineItem> LineItems { get; set; } = [];
}
