using AGInventoryManagement.Application.Common.Interfaces;
using AGInventoryManagement.Domain.Common;
using AGInventoryManagement.Domain.Products;

namespace AGInventoryManagement.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(Guid ProductId) : IRequest<DomainResult>;

public class DeleteProductCommandHandler(IApplicationDbContext context) 
    : IRequestHandler<DeleteProductCommand, DomainResult>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<DomainResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync([command.ProductId], cancellationToken);

        if (product is null)
        {
            return DomainResult.Failure(ProductErrors.NotFound);
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);

        return DomainResult.Success();
    }
}
