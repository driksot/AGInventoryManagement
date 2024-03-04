using AGInventoryManagement.Domain.Common;

namespace AGInventoryManagement.Domain.Stocks.Events;

public class QuantityOnHandUpdatedEvent(Stock stock) : BaseEvent
{
    public Stock Stock { get; } = stock;
}
