namespace AGInventoryManagement.Application.Customers.Queries.GetCustomerList;

public record CustomerResponse(Guid Id, string Name, string PhoneNumber, string Email);
