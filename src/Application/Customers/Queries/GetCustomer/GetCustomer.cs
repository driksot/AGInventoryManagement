using AGInventoryManagement.Application.Common.Interfaces;
using AGInventoryManagement.Application.Customers.Queries.GetCustomerList;
using AGInventoryManagement.Domain.Common;
using AGInventoryManagement.Domain.Customers;

namespace AGInventoryManagement.Application.Customers.Queries.GetCustomer;

public record GetCustomerQuery(Guid CustomerId) : IRequest<DomainResult<CustomerResponse>>;

public class GetCustomerQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetCustomerQuery, DomainResult<CustomerResponse>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<DomainResult<CustomerResponse>> Handle(GetCustomerQuery query, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers
            .Where(c => c.Id == query.CustomerId)
            .Select(c => new CustomerResponse(
                c.Id,
                string.Join(" ", c.FirstName, c.LastName),
                c.PhoneNumber,
                c.Email))
            .FirstOrDefaultAsync(cancellationToken);

        return customer is null
            ? DomainResult.Failure<CustomerResponse>(CustomerErrors.NotFound)
            : customer;
    }
}
