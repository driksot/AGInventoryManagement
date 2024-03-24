using AGInventoryManagement.Application.Common.Interfaces;
using AGInventoryManagement.Domain.Common;
using AGInventoryManagement.Domain.Products;

namespace AGInventoryManagement.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string? Description,
    decimal Price,
    string Sku) : IRequest<DomainResult<Guid>>;

public class CreateProductCommandHandler(IApplicationDbContext context) 
    : IRequestHandler<CreateProductCommand, DomainResult<Guid>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<DomainResult<Guid>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // Create a product
        var createProductResult = Product.Create(
            command.Name,
            command.Description,
            command.Price);

        if (createProductResult.IsFailure)
        {
            return DomainResult.Failure<Guid>(createProductResult.Error);
        }

        var product = createProductResult.Value;

        // Add product to database
        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);

        // Return product ID
        return product.Id;
    }
}
