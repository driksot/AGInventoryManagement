using AGInventoryManagement.Application.Common.Interfaces;
using AGInventoryManagement.Domain.Products;
using AGInventoryManagement.Domain.Stocks;

namespace AGInventoryManagement.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string? Description,
    decimal Price,
    string Sku) : IRequest<Guid>;

public class CreateProductCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Sku = Sku.Create(request.Sku)!
        };

        var stock = new Stock
        {
            ProductId = product.Id,
            QuantityOnHand = 0,
            IdealQuantity = 0,
        };

        product.Stock = stock;

        _context.Products.Add(product);

        await _context.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
