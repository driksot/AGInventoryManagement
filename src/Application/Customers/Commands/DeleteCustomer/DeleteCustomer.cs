using AGInventoryManagement.Application.Common.Interfaces;
using AGInventoryManagement.Domain.Common;
using AGInventoryManagement.Domain.Customers;

namespace AGInventoryManagement.Application.Customers.Commands.DeleteCustomer;

public record DeleteCustomerCommand(Guid CustomerId) : IRequest<DomainResult>;

public class DeleteCustomerCommandHandler(IApplicationDbContext context) 
    : IRequestHandler<DeleteCustomerCommand, DomainResult>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<DomainResult> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers.FindAsync([command.CustomerId], cancellationToken);

        if (customer is null)
        {
            return DomainResult.Failure(CustomerErrors.NotFound);
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync(cancellationToken);

        return DomainResult.Success();
    }
}
