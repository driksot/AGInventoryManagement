using AGInventoryManagement.Application.Common.Interfaces;
using AGInventoryManagement.Domain.Products;

namespace AGInventoryManagement.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand(
    Guid ProductId,
    string Name,
    string? Description,
    decimal Price,
    string Sku) : IRequest;

public class UpdateProductCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateProductCommand>
{
    private readonly IApplicationDbContext _context = context;

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync([request.ProductId], cancellationToken);

        Guard.Against.NotFound(request.ProductId, product);

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.Sku = Sku.Create(request.Sku)!;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
