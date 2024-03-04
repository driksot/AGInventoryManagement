using AGInventoryManagement.Application.Common.Interfaces;
using AGInventoryManagement.Domain.Stocks;

namespace AGInventoryManagement.Application.Stocks.Commands.UpdateStockOnHand;

public record UpdateStockOnHandCommand(Guid ProductId, int Adjustment) : IRequest;

public class UpdateStockOnHandCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateStockOnHandCommand>
{
    private readonly IApplicationDbContext _context = context;

    public async Task Handle(UpdateStockOnHandCommand request, CancellationToken cancellationToken)
    {
        var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.ProductId == request.ProductId, cancellationToken);

        Guard.Against.NotFound(request.ProductId, stock);

        stock.QuantityOnHand += request.Adjustment;

        //var snapshot = new StockSnapshot(
        //    Guid.NewGuid(),
        //    stock.Id,
        //    stock.ProductId,
        //    DateTime.UtcNow,
        //    stock.QuantityOnHand);

        //stock.Snapshots.Add(snapshot);

        //_context.StockSnapshots.Add(snapshot);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
