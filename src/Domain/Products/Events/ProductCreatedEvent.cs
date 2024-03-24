using AGInventoryManagement.Domain.Common;

namespace AGInventoryManagement.Domain.Products.Events;

public class ProductCreatedEvent(Product product) : BaseEvent
{
    public Product Product { get; } = product;
}
