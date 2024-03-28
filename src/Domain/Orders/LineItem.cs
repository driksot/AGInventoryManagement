using AGInventoryManagement.Domain.Common;

namespace AGInventoryManagement.Domain.Orders;

public class LineItem : BaseEntity
{
    public LineItem(Guid id, Guid orderId, Guid productId, decimal unitPrice) 
        : base(id)
    {
        OrderId = orderId;
        ProductId = productId;
        UnitPrice = unitPrice;
    }

    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    public decimal UnitPrice { get; set; }
}
