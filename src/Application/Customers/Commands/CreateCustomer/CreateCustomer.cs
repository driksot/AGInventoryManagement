using AGInventoryManagement.Application.Common.Interfaces;
using AGInventoryManagement.Domain.Common;
using AGInventoryManagement.Domain.Customers;

namespace AGInventoryManagement.Application.Customers.Commands.CreateCustomer;

public record CreateCustomerCommand(
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Email) : IRequest<DomainResult<Guid>>;

public class CreateCustomerCommandHandler(IApplicationDbContext context)
    : IRequestHandler<CreateCustomerCommand, DomainResult<Guid>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<DomainResult<Guid>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        // Create a customer
        var createCustomerResult = Customer.Create(
            command.FirstName,
            command.LastName,
            command.PhoneNumber,
            command.Email);

        if (createCustomerResult.IsFailure)
        {
            return DomainResult.Failure<Guid>(createCustomerResult.Error);
        }

        var customer = createCustomerResult.Value;

        // Add customer to database
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync(cancellationToken);

        // Return customer ID
        return customer.Id;
    }
}
