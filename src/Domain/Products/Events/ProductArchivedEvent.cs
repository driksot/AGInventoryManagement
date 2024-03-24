using AGInventoryManagement.Domain.Common;

namespace AGInventoryManagement.Domain.Products.Events;

public class ProductArchivedEvent(Product product) : BaseEvent
{
    public Product Product { get; } = product;
}
