using AGInventoryManagement.WebClient.Areas.Customers.Contracts;
using Microsoft.AspNetCore.Components;

namespace AGInventoryManagement.WebClient.Areas.Customers.Pages.Components;
public partial class CustomerEditForm
{
    [Parameter]
    public CustomerVM Customer { get; set; } = null!;

    [Parameter]
    public EventCallback<CustomerVM> OnValidSubmit { get; set; }

    public async Task HandleValidSubmit()
    {
        await OnValidSubmit.InvokeAsync(Customer);
    }
}
