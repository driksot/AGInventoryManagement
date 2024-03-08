using System.ComponentModel.DataAnnotations;

namespace AGInventoryManagement.WebClient.Areas.Identity.Contracts;

public class LoginUser
{
    [EmailAddress, Required]
    public string? Email { get; set; }

    [DataType(DataType.Password), Required]
    public string? Password { get; set; }
}
