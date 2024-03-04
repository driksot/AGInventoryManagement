using AGInventoryManagement.Application.Common.Interfaces;
using AGInventoryManagement.Application.Products.Queries.GetProductList;

namespace AGInventoryManagement.Application.Products.Queries.GetProduct;

public record GetProductQuery(Guid ProductId) : IRequest<ProductDto>;

public class GetProductQueryHandler(IApplicationDbContext context) : IRequestHandler<GetProductQuery, ProductDto>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .Where(p => p.Id == request.ProductId)
            .Include(p => p.Stock)
            .Select(p => new ProductDto()
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Sku = p.Sku.Value,
                StockOnHand = p.Stock.QuantityOnHand,
                StockIdeal = p.Stock.IdealQuantity
            })
            .FirstOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.ProductId, product);

        return product;
    }
}
