using AGInventoryManagement.Domain.Common;

namespace AGInventoryManagement.Domain.Customers;

public static class CustomerErrors
{
    public static Error NotFound = new(
        "Customer.NotFound",
        "The customer with the specified ID was not found.");
}
