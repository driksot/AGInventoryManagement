using System.Text.Json;
using AGInventoryManagement.WebClient.Areas.Common.Contracts;
using AGInventoryManagement.WebClient.Areas.Common.Services;
using AGInventoryManagement.WebClient.Areas.Products.Contracts;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.WebUtilities;

namespace AGInventoryManagement.WebClient.Areas.Products.Services;

public class ProductService(
    HttpClient httpClient,
    ILocalStorageService localStorage) 
    : BaseHttpService(httpClient, localStorage), IProductService
{
    private const string _productUrl = "api/Products";
    private readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

    public async Task<PagingResponse<ProductResponse>> GetProductListAsync(PaginationRequest pagination)
    {
        var queryStringParam = new Dictionary<string, string>()
        {
            ["page"] = pagination.Page.ToString(),
            ["pageSize"] = pagination.PageSize.ToString(),
        };

        if (pagination.SearchTerm is not null) queryStringParam.Add("searchTerm", pagination.SearchTerm);
        if (pagination.SortColumn is not null) queryStringParam.Add("sortColumn", pagination.SortColumn);
        if (pagination.SortOrder is not null) queryStringParam.Add("sortOrder", pagination.SortOrder);

        var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString(_productUrl, queryStringParam!));
        var content = await response.Content.ReadAsStringAsync();

        var products = JsonSerializer.Deserialize<PagingResponse<ProductResponse>>(content, _options);
        Console.Write("Response from API = " + products);

        if (products is null) throw new Exception();

        return products;
    }
}
