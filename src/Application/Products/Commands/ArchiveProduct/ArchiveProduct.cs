using AGInventoryManagement.Application.Common.Interfaces;

namespace AGInventoryManagement.Application.Products.Commands.ArchiveProduct;

public record ArchiveProductCommand(Guid ProductId) : IRequest;

public class ArchiveProductCommandHandler(IApplicationDbContext context) : IRequestHandler<ArchiveProductCommand>
{
    private readonly IApplicationDbContext _context = context;

    public async Task Handle(ArchiveProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);

        if (product is null) return;

        product.IsArchived = true;

        _context.Products.Update(product);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
