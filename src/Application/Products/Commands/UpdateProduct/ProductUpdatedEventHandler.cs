using AGInventoryManagement.Domain.Products.Events;
using Microsoft.Extensions.Logging;

namespace AGInventoryManagement.Application.Products.Commands.UpdateProduct;

public class ProductUpdatedEventHandler(ILogger<ProductUpdatedEventHandler> logger) : INotificationHandler<ProductUpdatedEvent>
{
    private readonly ILogger<ProductUpdatedEventHandler> _logger = logger;

    public Task Handle(ProductUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("AGInventoryManagement Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
