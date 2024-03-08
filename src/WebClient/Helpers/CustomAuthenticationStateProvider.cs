using System.Security.Claims;
using AGInventoryManagement.WebClient.Areas.Identity.Contracts;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace AGInventoryManagement.WebClient.Helpers;

public class CustomAuthenticationStateProvider(ILocalStorageService localStorageService) : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService = localStorageService;
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var authenticationModel = await _localStorageService.GetItemAsStringAsync("Authentication");
            if (authenticationModel == null) { return await Task.FromResult(new AuthenticationState(_anonymous)); }
            return await Task.FromResult(new AuthenticationState(SetClaims(Deserialize(authenticationModel).Username!)));
        }
        catch
        {
            return await Task.FromResult(new AuthenticationState(_anonymous));
        }
    }

    public async Task UpdateAuthenticationState(AuthenticationModel authenticationModel)
    {
        try
        {
            ClaimsPrincipal claimsPrincipal = new();
            if (authenticationModel is not null)
            {
                claimsPrincipal = SetClaims(authenticationModel.Username!);
                await _localStorageService.SetItemAsStringAsync("Authentication", Serialize(authenticationModel));
            }
            else
            {
                await _localStorageService.RemoveItemAsync("Authentication");
                claimsPrincipal = _anonymous;
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
        catch
        {
            await Task.FromResult(new AuthenticationState(_anonymous));
        }
    }

    private ClaimsPrincipal SetClaims(string email) => new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
        {
            new(ClaimTypes.Name, email)
        }, "CustomAuth"));

    private static AuthenticationModel Deserialize(string serializeString) => JsonSerializer.Deserialize<AuthenticationModel>(serializeString)!;

    private static string Serialize(AuthenticationModel model) => JsonSerializer.Serialize(model);
}
