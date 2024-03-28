namespace AGInventoryManagement.Application.Customers.Commands.CreateCustomer;

public record CreateCustomerRequest(
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Email);
