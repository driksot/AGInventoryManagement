using AGInventoryManagement.Domain.Products.Events;
using Microsoft.Extensions.Logging;

namespace AGInventoryManagement.Application.Products.Commands.CreateProduct;

public class ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger) 
    : INotificationHandler<ProductArchivedEvent>
{
    private readonly ILogger<ProductCreatedEventHandler> _logger = logger;

    public Task Handle(ProductArchivedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("AGInventoryManagement Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
