namespace AGInventoryManagement.WebClient.Areas.Customers.Contracts;

public record CustomerResponse(
    Guid Id,
    string Name,
    string PhoneNumber,
    string Email);
