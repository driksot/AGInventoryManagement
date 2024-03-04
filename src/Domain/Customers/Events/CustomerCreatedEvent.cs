using AGInventoryManagement.Domain.Common;

namespace AGInventoryManagement.Domain.Customers.Events;

public class CustomerCreatedEvent(Customer customer) : BaseEvent
{
    public Customer Customer { get; } = customer;
}
