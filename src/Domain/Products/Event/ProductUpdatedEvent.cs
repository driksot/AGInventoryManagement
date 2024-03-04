using AGInventoryManagement.Domain.Common;

namespace AGInventoryManagement.Domain.Products.Event;

public class ProductUpdatedEvent(Product product) : BaseEvent
{
    public Product Product { get; } = product;
}
