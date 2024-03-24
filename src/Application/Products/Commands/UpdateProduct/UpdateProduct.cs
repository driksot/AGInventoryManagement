using AGInventoryManagement.Application.Common.Interfaces;
using AGInventoryManagement.Domain.Common;
using AGInventoryManagement.Domain.Products;

namespace AGInventoryManagement.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand(
    Guid ProductId,
    string Name,
    string Description,
    decimal Price) : IRequest<DomainResult>;

public class UpdateProductCommandHandler(IApplicationDbContext context) 
    : IRequestHandler<UpdateProductCommand, DomainResult>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<DomainResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync([command.ProductId], cancellationToken);

        if (product is null)
        {
            return DomainResult.Failure(ProductErrors.NotFound);
        }

        var updateProductResult = product.Update(
            command.Name,
            command.Description,
            command.Price);

        if (updateProductResult.IsFailure)
        {
            return DomainResult.Failure(updateProductResult.Error);
        }

        _context.Products.Update(product);
        await _context.SaveChangesAsync(cancellationToken);

        return DomainResult.Success();
    }
}
