using System.Linq.Expressions;
using AGInventoryManagement.Application.Common.Interfaces;
using AGInventoryManagement.Application.Common.Models;
using AGInventoryManagement.Application.Products.Queries.GetProductList;
using AGInventoryManagement.Domain.Common;
using AGInventoryManagement.Domain.Products;

namespace AGInventoryManagement.Application.Stocks.Queries.GetSnapshotList;

public record GetSnapshotListQuery(
    string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageSize) : IRequest<DomainResult<PaginatedList<SnapshotDto>>>;

public class GetSnapshotListQueryHandler(IApplicationDbContext context) 
    : IRequestHandler<GetSnapshotListQuery, DomainResult<PaginatedList<SnapshotDto>>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<DomainResult<PaginatedList<SnapshotDto>>> Handle(GetSnapshotListQuery query, CancellationToken cancellationToken)
    {
        var snapshots = await _context.StockSnapshots.ToListAsync(cancellationToken);

        var snapshotDtos = new List<SnapshotDto>();

        foreach (var snapshot in snapshots)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == snapshot.ProductId);

            if (product is null)
            {
                return DomainResult.Failure<PaginatedList<SnapshotDto>>(ProductErrors.NotFound);
            }

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

        var snapshotsQuery = snapshotDtos.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            snapshotsQuery = snapshotsQuery.Where(p =>
                p.Product.Name.Contains(query.SearchTerm) ||
                ((string)p.Product.Sku).Contains(query.SearchTerm));
        }

        if (query.SortOrder?.ToLower() == "desc")
        {
            snapshotsQuery = snapshotsQuery.OrderByDescending(GetSortProperty(query));
        }
        else
        {
            snapshotsQuery = snapshotsQuery.OrderBy(GetSortProperty(query));
        }

        var pagedProducts = await PaginatedList<SnapshotDto>.CreateAsync(
            snapshotsQuery,
            query.Page,
            query.PageSize);

        return pagedProducts;
    }

    private static Expression<Func<SnapshotDto, object>> GetSortProperty(GetSnapshotListQuery query) =>
        query.SortColumn?.ToLower() switch
        {
            "name" => snapshot => snapshot.Product.Name,
            "sku" => snapshot => snapshot.Product.Sku,
            "price" => snapshot => snapshot.Product.Price,
            "stock" => snapshot => snapshot.Quantity,
            _ => snapshot => snapshot.Id
        };
}
