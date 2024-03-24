using AGInventoryManagement.Domain.Common;

namespace AGInventoryManagement.Domain.Products.Events;

public class ProductUpdatedEvent(Product product) : BaseEvent
{
    public Product Product { get; } = product;
}
