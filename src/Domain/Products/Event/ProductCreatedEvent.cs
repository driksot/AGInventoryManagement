using AGInventoryManagement.Domain.Common;

namespace AGInventoryManagement.Domain.Products.Event;

public class ProductCreatedEvent(Product product) : BaseEvent
{
    public Product Product { get; } = product;
}
