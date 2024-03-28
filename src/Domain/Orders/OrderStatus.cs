using Ardalis.SmartEnum;

namespace AGInventoryManagement.Domain.Orders;

public abstract class OrderStatus : SmartEnum<OrderStatus>
{
    public static readonly OrderStatus Generated = new GeneratedStatus();
    public static readonly OrderStatus InProgress = new InProgressStatus();
    public static readonly OrderStatus Paid = new PaidStatus();
    public static readonly OrderStatus Cancelled = new CancelledStatus();

    protected OrderStatus(string name, int value) : base(name, value)
    {
    }

    public abstract bool CanTransitionTo(OrderStatus next);

    private sealed class GeneratedStatus : OrderStatus
    {
        public GeneratedStatus() : base("Generated", 0)
        {
        }

        public override bool CanTransitionTo(OrderStatus next) => 
            next == OrderStatus.InProgress || next == OrderStatus.Cancelled;
    }

    private sealed class InProgressStatus : OrderStatus
    {
        public InProgressStatus() : base("Completed", 1)
        {
        }

        public override bool CanTransitionTo(OrderStatus next) => 
            next == OrderStatus.Paid || next == OrderStatus.Cancelled;
    }

    private sealed class PaidStatus : OrderStatus
    {
        public PaidStatus() : base("Paid", 2)
        {
        }

        public override bool CanTransitionTo(OrderStatus next) =>
            next == OrderStatus.Cancelled;
    }

    private sealed class CancelledStatus : OrderStatus
    {
        public CancelledStatus() : base("Cancelled", 3)
        {
        }

        public override bool CanTransitionTo(OrderStatus next) =>
            false;
    }
}
