using System.Linq.Expressions;
using AGInventoryManagement.Application.Common.Interfaces;
using AGInventoryManagement.Application.Common.Models;
using AGInventoryManagement.Domain.Common;
using AGInventoryManagement.Domain.Products;

namespace AGInventoryManagement.Application.Products.Queries.GetProductList;

public record GetProductListQuery(
    string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageSize) : IRequest<DomainResult<PaginatedList<ProductDto>>>;

public class GetProductListQueryHandler(IApplicationDbContext context) : IRequestHandler<GetProductListQuery, DomainResult<PaginatedList<ProductDto>>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<DomainResult<PaginatedList<ProductDto>>> Handle(GetProductListQuery query, CancellationToken cancellationToken)
    {
        IQueryable<Product> productsQuery = _context.Products;

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

        var products = await PaginatedList<ProductDto>.CreateAsync(
            productResponseQuery,
            query.Page,
            query.PageSize);

        return products;
    }

    private static Expression<Func<Product, object>> GetSortProperty(GetProductListQuery query) =>
        query.SortColumn?.ToLower() switch
        {
            "name" => product => product.Name,
            "sku" => product => product.Sku,
            "price" => product => product.Price,
            "stock" => product => product.Stock.QuantityOnHand,
            "ideal" => product => product.Stock.IdealQuantity,
            _ => product => product.Id
        };
}
