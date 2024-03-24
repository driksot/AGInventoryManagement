using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace AGInventoryManagement.WebClient.Areas.Common.Services;

public class BaseHttpService(HttpClient httpClient, ILocalStorageService localStorage)
{
    protected readonly HttpClient _httpClient = httpClient;
    protected readonly ILocalStorageService _localStorage = localStorage;

    protected async Task AddBearerToken()
    {
        if (await _localStorage.ContainKeyAsync("token"))
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                await _localStorage.GetItemAsync<string>("token"));
    }
}
