using AGInventoryManagement.WebClient.Areas.Identity.Services;
using AGInventoryManagement.WebClient.Helpers;
using Microsoft.AspNetCore.Components;

namespace AGInventoryManagement.WebClient.Layout;
public partial class LoginDisplay
{
    [Inject]
    public IAuthService AuthService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await ((CustomAuthenticationStateProvider)authStateProvider).GetAuthenticationStateAsync();
    }

    protected void GoToLogin()
    {
        NavManager.NavigateTo("login/");
    }

    protected void GoToRegister()
    {
        NavManager.NavigateTo("register/");
    }

    protected void Logout()
    {
        //await AuthService

        NavManager.NavigateTo("/");
    }
}
