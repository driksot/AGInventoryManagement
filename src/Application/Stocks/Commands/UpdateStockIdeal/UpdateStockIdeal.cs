using AGInventoryManagement.Application.Common.Interfaces;

namespace AGInventoryManagement.Application.Stocks.Commands.UpdateStockIdeal;

public record UpdateStockIdealCommand(Guid ProductId, int Adjustment) : IRequest;

public class UpdateStockIdealCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateStockIdealCommand>
{
    private readonly IApplicationDbContext _context = context;

    public async Task Handle(UpdateStockIdealCommand request, CancellationToken cancellationToken)
    {
        var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.ProductId == request.ProductId, cancellationToken);

        Guard.Against.NotFound(request.ProductId, stock);

        stock.IdealQuantity += request.Adjustment;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
