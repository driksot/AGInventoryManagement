using AGInventoryManagement.Application.Products.Commands.CreateProduct;
using AGInventoryManagement.Domain.Products.Event;
using Microsoft.Extensions.Logging;

namespace AGInventoryManagement.Application.Products.Commands.ArchiveProduct;

public class ProductArchivedEventHandler(ILogger<ProductCreatedEventHandler> logger) : INotificationHandler<ProductArchivedEvent>
{
    private readonly ILogger<ProductCreatedEventHandler> _logger = logger;

    public Task Handle(ProductArchivedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("AGInventoryManagement Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
