using AGInventoryManagement.WebClient.Areas.Identity.Contracts;

namespace AGInventoryManagement.WebClient.Areas.Identity.Services;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginUser user);
}
