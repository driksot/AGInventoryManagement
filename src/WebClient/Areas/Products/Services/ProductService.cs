using System.Text;
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
    private const string _mediaType = "application/json";
    private readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

    public async Task<ProductResponse> GetProductByIdAsync(Guid productId)
    {
        var response = await _httpClient.GetAsync($"{_productUrl}/{productId}");
        var content = await response.Content.ReadAsStringAsync();

        var product = JsonSerializer.Deserialize<ProductResponse>(content, _options);

        if (product is null) throw new Exception();

        return product;
    }

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

    public async Task CreateProductAsync(ProductVM product)
    {
        var content = JsonSerializer.Serialize(product);
        var bodyContent = new StringContent(content, Encoding.UTF8, _mediaType);

        var postResult = await _httpClient.PostAsync(_productUrl, bodyContent);
        var postContent = await postResult.Content.ReadAsStringAsync();

        if (!postResult.IsSuccessStatusCode)
        {
            throw new ApplicationException(postContent);
        }
    }

    public async Task UpdateProductAsync(ProductVM product)
    {
        var content = JsonSerializer.Serialize(product);
        var bodyContent = new StringContent(content, Encoding.UTF8, _mediaType);
        var url = Path.Combine(_productUrl, product.Id.ToString());

        var putResult = await _httpClient.PutAsync(url, bodyContent);
        var putContent = await putResult.Content.ReadAsStringAsync();

        if (!putResult.IsSuccessStatusCode)
        {
            throw new ApplicationException(putContent);
        }
    }

    public async Task DeleteProductAsync(Guid productId)
    {
        var url = Path.Combine(_productUrl, productId.ToString());

        var deleteResult = await _httpClient.DeleteAsync(url);
        var deleteContent = await deleteResult.Content.ReadAsStringAsync();

        if (!deleteResult.IsSuccessStatusCode)
        {
            throw new ApplicationException(deleteContent);
        }
    }
}
