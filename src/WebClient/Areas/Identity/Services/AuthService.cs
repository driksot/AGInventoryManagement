using System.Net.Http.Json;
using System.Text.Json;
using AGInventoryManagement.WebClient.Areas.Common.Services;
using AGInventoryManagement.WebClient.Areas.Identity.Contracts;
using AGInventoryManagement.WebClient.Helpers;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace AGInventoryManagement.WebClient.Areas.Identity.Services;

public class AuthService(
    HttpClient httpClient,
    ILocalStorageService localStorage,
    AuthenticationStateProvider authenticationStateProvider) 
    : BaseHttpService(httpClient, localStorage), IAuthService
{
    private const string _authUrl = "api/Users";
    private readonly AuthenticationStateProvider _authenticationStateProvider = authenticationStateProvider;
    private readonly JsonSerializerOptions _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    public async Task<LoginResponse> LoginAsync(LoginUser user)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_authUrl}/login", user);
        if (!response.IsSuccessStatusCode) return new LoginResponse();

        var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
        if (string.IsNullOrEmpty(result!.AccessToken)) return new LoginResponse();

        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.AccessToken);
        var getUserClaims = await _httpClient.GetAsync($"{_authUrl}/manage/info");
        if (!getUserClaims.IsSuccessStatusCode) return new LoginResponse();

        var userDetails = await getUserClaims.Content.ReadFromJsonAsync<UserDetails>();
        var authenticationModel = new AuthenticationModel()
        {
            Token = result.AccessToken,
            RefreshToken = result.RefreshToken,
            Username = userDetails!.Email
        };

        var customAuthStateProvider = (CustomAuthenticationStateProvider)_authenticationStateProvider;
        await customAuthStateProvider.UpdateAuthenticationState(authenticationModel);

        return result;
    }


}
