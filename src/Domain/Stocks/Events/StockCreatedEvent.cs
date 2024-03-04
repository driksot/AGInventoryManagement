using AGInventoryManagement.Domain.Common;

namespace AGInventoryManagement.Domain.Stocks.Events;

public class StockCreatedEvent(Stock stock) : BaseEvent
{
    public Stock Stock { get; } = stock;
}
