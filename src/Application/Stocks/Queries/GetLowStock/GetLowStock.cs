using AGInventoryManagement.Application.Common.Interfaces;
using AGInventoryManagement.Application.Common.Mappings;
using AGInventoryManagement.Application.Common.Models;
using AGInventoryManagement.Application.Products.Queries.GetProductList;

namespace AGInventoryManagement.Application.Stocks.Queries.GetLowStock;

public record GetLowStockQuery : IRequest<PaginatedList<ProductDto>>
{
    public int PageNumber { get; init; } = 1;

    public int PageSize { get; init; } = 10;
}

public class GetLowStockQueryValidator : AbstractValidator<GetLowStockQuery>
{
    public GetLowStockQueryValidator()
    {
    }
}

public class GetLowStockQueryHandler : IRequestHandler<GetLowStockQuery, PaginatedList<ProductDto>>
{
    private readonly IApplicationDbContext _context;

    public GetLowStockQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<ProductDto>> Handle(GetLowStockQuery request, CancellationToken cancellationToken)
    {
        return await _context.Products
            .Include(p => p.Stock)
            .Where(p => p.IsArchived == false && p.Stock.QuantityOnHand < p.Stock.IdealQuantity)
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
