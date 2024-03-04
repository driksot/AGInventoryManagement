using System.Linq.Expressions;
using AGInventoryManagement.Application.Common.Interfaces;
using AGInventoryManagement.Application.Common.Mappings;
using AGInventoryManagement.Application.Common.Models;
using AGInventoryManagement.Domain.Products;

namespace AGInventoryManagement.Application.Products.Queries.GetProductList;

public record GetProductListQuery : IRequest<PaginatedList<ProductDto>>
{
    public int PageNumber { get; init; } = 1;

    public int PageSize { get; init; } = 10;
}

public class GetProductListQueryHandler(IApplicationDbContext context) : IRequestHandler<GetProductListQuery, PaginatedList<ProductDto>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<PaginatedList<ProductDto>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
    {
        return await _context.Products
            .Where(p => p.IsArchived == false)
            .Include(p => p.Stock)
            .OrderBy(p => p.Name)
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
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
