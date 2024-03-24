using System.Linq.Expressions;
using AGInventoryManagement.Application.Common.Interfaces;
using AGInventoryManagement.Application.Common.Models;
using AGInventoryManagement.Application.Products.Queries.GetProductList;
using AGInventoryManagement.Domain.Common;
using AGInventoryManagement.Domain.Products;

namespace AGInventoryManagement.Application.Stocks.Queries.GetLowStock;

public record GetLowStockQuery(
    string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageSize) : IRequest<DomainResult<PaginatedList<ProductDto>>>;

public class GetLowStockQueryValidator : AbstractValidator<GetLowStockQuery>
{
    public GetLowStockQueryValidator()
    {
    }
}

public class GetLowStockQueryHandler(IApplicationDbContext context) 
    : IRequestHandler<GetLowStockQuery, DomainResult<PaginatedList<ProductDto>>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<DomainResult<PaginatedList<ProductDto>>> Handle(GetLowStockQuery query, CancellationToken cancellationToken)
    {
        IQueryable<Product> productsQuery = _context.Products
            .Include(p => p.Stock)
            .Where(p => p.Stock!.QuantityOnHand < p.Stock!.IdealQuantity);

        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            productsQuery = productsQuery.Where(p =>
                p.Name.Contains(query.SearchTerm) ||
                ((string)p.Sku).Contains(query.SearchTerm));
        }

        if (query.SortOrder?.ToLower() == "desc")
        {
            productsQuery = productsQuery.OrderByDescending(GetSortProperty(query));
        }
        else
        {
            productsQuery = productsQuery.OrderBy(GetSortProperty(query));
        }

        var productResponseQuery = productsQuery
            .Select(p => new ProductDto()
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Sku = p.Sku.Value,
                StockOnHand = p.Stock.QuantityOnHand,
                StockIdeal = p.Stock.IdealQuantity
            });

        var pagedProducts = await PaginatedList<ProductDto>.CreateAsync(
            productResponseQuery,
            query.Page,
            query.PageSize);

        return pagedProducts;
    }

    private static Expression<Func<Product, object>> GetSortProperty(GetLowStockQuery query) =>
        query.SortColumn?.ToLower() switch
        {
            "name" => product => product.Name,
            "sku" => product => product.Sku,
            "price" => product => product.Price,
            "stock" => product => product.Stock!.QuantityOnHand,
            "ideal" => product => product.Stock!.IdealQuantity,
            _ => product => product.Id
        };
}
