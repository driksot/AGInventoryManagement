using System.ComponentModel.DataAnnotations;

namespace AGInventoryManagement.WebClient.Areas.Customers.Contracts;

public class CustomerVM
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "First Name is a required field")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last Name is a required field")]
    public string LastName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
}
