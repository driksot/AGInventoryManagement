using AGInventoryManagement.Application.Common.Interfaces;
using AGInventoryManagement.Application.Products.Queries.GetProductList;
using AGInventoryManagement.Domain.Common;
using AGInventoryManagement.Domain.Products;

namespace AGInventoryManagement.Application.Products.Queries.GetProduct;

public record GetProductQuery(Guid ProductId) : IRequest<DomainResult<ProductDto>>;

public class GetProductQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetProductQuery, DomainResult<ProductDto>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<DomainResult<ProductDto>> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .Where(p => p.Id == query.ProductId)
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

        return product is null
            ? DomainResult.Failure<ProductDto>(ProductErrors.NotFound)
            : product;
    }
}
