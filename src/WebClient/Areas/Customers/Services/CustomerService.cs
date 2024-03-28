using System.Text;
using System.Text.Json;
using AGInventoryManagement.WebClient.Areas.Common.Contracts;
using AGInventoryManagement.WebClient.Areas.Common.Services;
using AGInventoryManagement.WebClient.Areas.Customers.Contracts;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.WebUtilities;

namespace AGInventoryManagement.WebClient.Areas.Customers.Services;

public class CustomerService(HttpClient httpClient, ILocalStorageService loccalStorage)
    : BaseHttpService(httpClient, loccalStorage), ICustomerService
{
    private const string _customerUrl = "api/Customers";
    private const string _mediaType = "application/json";
    private readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

    public async Task<CustomerResponse> GetCustomerByIdAsync(Guid customerId)
    {
        var url = Path.Combine(_customerUrl, customerId.ToString());

        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        var customer = JsonSerializer.Deserialize<CustomerResponse>(content, _options);

        if (customer is null) throw new Exception();

        return customer;
    }

    public async Task<PagingResponse<CustomerResponse>> GetCustomerListAsync(PaginationRequest pagination)
    {
        var queryStringParam = new Dictionary<string, string>()
        {
            ["page"] = pagination.Page.ToString(),
            ["pageSize"] = pagination.PageSize.ToString(),
        };

        if (pagination.SearchTerm is not null) queryStringParam.Add("searchTerm", pagination.SearchTerm);
        if (pagination.SortColumn is not null) queryStringParam.Add("sortColumn", pagination.SortColumn);
        if (pagination.SortOrder is not null) queryStringParam.Add("sortOrder", pagination.SortOrder);

        var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString(_customerUrl, queryStringParam!));
        var content = await response.Content.ReadAsStringAsync();

        var customers = JsonSerializer.Deserialize<PagingResponse<CustomerResponse>>(content, _options);

        if (customers is null) throw new Exception();

        return customers;
    }

    public async Task<List<CustomerResponse>> GetCustomerListByNameAsync(string name)
    {
        var url = Path.Combine(_customerUrl, "name", name);

        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        var customers = JsonSerializer.Deserialize<List<CustomerResponse>>(content, _options);

        if (customers is null) throw new Exception();

        return customers;
    }

    public async Task CreateCustomerAsync(CustomerVM customer)
    {
        var content = JsonSerializer.Serialize(customer);
        var bodyContent = new StringContent(content, Encoding.UTF8, _mediaType);

        var postResult = await _httpClient.PostAsync(_customerUrl, bodyContent);
        var postContent = await postResult.Content.ReadAsStringAsync();

        if (!postResult.IsSuccessStatusCode)
        {
            throw new ApplicationException(postContent);
        }
    }

    public async Task DeleteCustomerAsync(Guid customerId)
    {
        var url = Path.Combine(_customerUrl, customerId.ToString());

        var deleteResult = await _httpClient.DeleteAsync(url);
        var deleteContent = await deleteResult.Content.ReadAsStringAsync();

        if (!deleteResult.IsSuccessStatusCode)
        {
            throw new ApplicationException(deleteContent);
        }
    }
}
