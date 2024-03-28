using System.Linq.Expressions;
using AGInventoryManagement.Application.Common.Interfaces;
using AGInventoryManagement.Application.Common.Models;
using AGInventoryManagement.Application.Products.Queries.GetProductList;
using AGInventoryManagement.Domain.Common;
using AGInventoryManagement.Domain.Customers;
using AGInventoryManagement.Domain.Products;

namespace AGInventoryManagement.Application.Customers.Queries.GetCustomerList;

public record GetCustomerListQuery(
    string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageSize) : IRequest<DomainResult<PaginatedList<CustomerResponse>>>;

public class GetCustomerListQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetCustomerListQuery, DomainResult<PaginatedList<CustomerResponse>>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<DomainResult<PaginatedList<CustomerResponse>>> Handle(GetCustomerListQuery query, CancellationToken cancellationToken)
    {
        IQueryable<Customer> customersQuery = _context.Customers;

        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            customersQuery = customersQuery.Where(c =>
                c.FirstName.Contains(query.SearchTerm) ||
                c.LastName.Contains(query.SearchTerm));
        }

        if (query.SortOrder?.ToLower() == "desc")
        {
            customersQuery = customersQuery.OrderByDescending(GetSortProperty(query));
        }
        else
        {
            customersQuery = customersQuery.OrderBy(GetSortProperty(query));
        }

        var customerResponseQuery = customersQuery
            .Select(c => new CustomerResponse(
                c.Id,
                string.Join(" ", c.FirstName, c.LastName),
                c.PhoneNumber,
                c.Email));

        var customers = await PaginatedList<CustomerResponse>.CreateAsync(
            customerResponseQuery,
            query.Page,
            query.PageSize);

        return customers;
    }

    private static Expression<Func<Customer, object>> GetSortProperty(GetCustomerListQuery query) =>
        query.SortColumn?.ToLower() switch
        {
            "firstNam" => customer => customer.FirstName,
            "lastName" => customer => customer.LastName,
            "phoneNumber" => customer => customer.PhoneNumber,
            "email" => customer => customer.Email,
            _ => customer => customer.Id
        };
}
