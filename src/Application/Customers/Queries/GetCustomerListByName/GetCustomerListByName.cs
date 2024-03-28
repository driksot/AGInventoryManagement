using AGInventoryManagement.Application.Common.Interfaces;
using AGInventoryManagement.Application.Customers.Queries.GetCustomerList;
using AGInventoryManagement.Domain.Common;

namespace AGInventoryManagement.Application.Customers.Queries.GetCustomerListByName;

public record GetCustomerListByNameQuery(string Name)
    : IRequest<DomainResult<List<CustomerResponse>>>;

public class GetCustomerListByNameQueryHandler(IApplicationDbContext context) :
    IRequestHandler<GetCustomerListByNameQuery, DomainResult<List<CustomerResponse>>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<DomainResult<List<CustomerResponse>>> Handle(GetCustomerListByNameQuery query, CancellationToken cancellationToken)
    {
        var customers = await _context.Customers
            .Where(
                c => c.FirstName.IndexOf(query.Name, StringComparison.CurrentCultureIgnoreCase) > -1
                || c.LastName.IndexOf(query.Name, StringComparison.CurrentCultureIgnoreCase) > -1)
            .ToListAsync();

        return customers.Select(
            c => new CustomerResponse(
                c.Id,
                string.Join(" ", c.FirstName, c.LastName),
                c.PhoneNumber,
                c.Email))
        .ToList();
    }
}
