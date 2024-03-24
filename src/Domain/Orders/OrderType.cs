using Ardalis.SmartEnum;

namespace AGInventoryManagement.Domain.Orders;

public abstract class OrderType : SmartEnum<OrderType>
{
    public static readonly OrderType Sales = new SalesOrder();
    public static readonly OrderType Purchase = new PurchaseOrder();

    protected OrderType(string name, int value) : base(name, value)
    {
    }

    private sealed class SalesOrder : OrderType
    {
        public SalesOrder() : base("Sales", 0)
        {
        }
    }

    private sealed class PurchaseOrder : OrderType
    {
        public PurchaseOrder() : base("Purchase", 1)
        {
        }
    }
}
