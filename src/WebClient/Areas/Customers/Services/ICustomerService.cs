using AGInventoryManagement.WebClient.Areas.Common.Contracts;
using AGInventoryManagement.WebClient.Areas.Customers.Contracts;

namespace AGInventoryManagement.WebClient.Areas.Customers.Services;

public interface ICustomerService
{
    Task<CustomerResponse> GetCustomerByIdAsync(Guid customerId);
    Task<PagingResponse<CustomerResponse>> GetCustomerListAsync(PaginationRequest pagination);
    Task<List<CustomerResponse>> GetCustomerListByNameAsync(string name);
    Task CreateCustomerAsync(CustomerVM customer);
    Task DeleteCustomerAsync(Guid customerId);
}
