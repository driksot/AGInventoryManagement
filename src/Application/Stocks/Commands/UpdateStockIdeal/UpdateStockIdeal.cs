using AGInventoryManagement.Application.Common.Interfaces;
using AGInventoryManagement.Domain.Common;
using AGInventoryManagement.Domain.Stocks;

namespace AGInventoryManagement.Application.Stocks.Commands.UpdateStockIdeal;

public record UpdateStockIdealCommand(Guid ProductId, int Adjustment) : IRequest<DomainResult>;

public class UpdateStockIdealCommandHandler(IApplicationDbContext context) 
    : IRequestHandler<UpdateStockIdealCommand, DomainResult>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<DomainResult> Handle(UpdateStockIdealCommand command, CancellationToken cancellationToken)
    {
        var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.ProductId == command.ProductId, cancellationToken);

        if (stock is null)
        {
            return DomainResult.Failure(StockErrors.NotFound);
        }

        var updateStockIdealResult = stock.UpdateIdealQuantity(command.Adjustment);

        if (updateStockIdealResult.IsFailure)
        {
            return DomainResult.Failure(updateStockIdealResult.Error);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return DomainResult.Success();
    }
}
