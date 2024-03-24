using AGInventoryManagement.Application.Common.Interfaces;
using AGInventoryManagement.Domain.Stocks.Events;
using Microsoft.Extensions.Logging;

namespace AGInventoryManagement.Application.Stocks.Commands.UpdateStockOnHand;

public class QuantityOnHandUpdatedEventHandler(
    ILogger<QuantityOnHandUpdatedEventHandler> logger,
    IApplicationDbContext context)
    : INotificationHandler<QuantityOnHandUpdatedEvent>
{
    private readonly ILogger<QuantityOnHandUpdatedEventHandler> _logger = logger;
    private readonly IApplicationDbContext _context = context;

    public Task Handle(QuantityOnHandUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("AGInventoryManagement Domain Event: {DomainEvent}", notification.GetType().Name);

        //var snapshot = new StockSnapshot(
        //    Guid.NewGuid(),
        //    notification.Stock.Id,
        //    notification.Stock.ProductId,
        //    DateTime.Now,
        //    notification.Stock.QuantityOnHand);

        //_context.StockSnapshots.Add(snapshot);

        //await _context.SaveChangesAsync(cancellationToken);

        return Task.CompletedTask;
    }
}
