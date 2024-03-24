using AGInventoryManagement.Application.Common.Interfaces;
using AGInventoryManagement.Domain.Common;
using AGInventoryManagement.Domain.Stocks;

namespace AGInventoryManagement.Application.Stocks.Commands.UpdateStockOnHand;

public record UpdateStockOnHandCommand(Guid ProductId, int Adjustment) : IRequest<DomainResult>;

public class UpdateStockOnHandCommandHandler(IApplicationDbContext context) 
    : IRequestHandler<UpdateStockOnHandCommand, DomainResult>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<DomainResult> Handle(UpdateStockOnHandCommand command, CancellationToken cancellationToken)
    {
        var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.ProductId == command.ProductId, cancellationToken);

        if (stock is null)
        {
            return DomainResult.Failure(StockErrors.NotFound);
        }

        var updateStockOnHandResult = stock.UpdateQuantityOnHand(command.Adjustment);

        if (updateStockOnHandResult.IsFailure)
        {
            return DomainResult.Failure(updateStockOnHandResult.Error);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return DomainResult.Success();
    }
}
