using AGInventoryManagement.Application.Common.Interfaces;
using AGInventoryManagement.Application.Common.Mappings;
using AGInventoryManagement.Application.Common.Models;
using AGInventoryManagement.Application.Products.Queries.GetProductList;

namespace AGInventoryManagement.Application.Stocks.Queries.GetSnapshotList;

public record GetSnapshotListQuery : IRequest<PaginatedList<SnapshotDto>>
{
    public int PageNumber { get; init; } = 1;

    public int PageSize { get; init; } = 10;
}

public class GetSnapshotListQueryHandler(IApplicationDbContext context) : IRequestHandler<GetSnapshotListQuery, PaginatedList<SnapshotDto>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<PaginatedList<SnapshotDto>> Handle(GetSnapshotListQuery request, CancellationToken cancellationToken)
    {
        var snapshots = await _context.StockSnapshots.ToListAsync(cancellationToken);

        var snapshotDtos = new List<SnapshotDto>();

        foreach (var snapshot in snapshots)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == snapshot.ProductId);

            Guard.Against.NotFound(snapshot.ProductId, product);

            var response = new SnapshotDto()
            {
                Id = snapshot.Id,
                Product = new ProductDto()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Sku = product.Sku.Value,
                    StockOnHand = product.Stock.QuantityOnHand,
                    StockIdeal = product.Stock.IdealQuantity
                },
                SnapshotTime = snapshot.SnapshotTime,
                Quantity = snapshot.Quantity
            };

            snapshotDtos.Add(response);
        }

        return await snapshotDtos.AsQueryable().PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
