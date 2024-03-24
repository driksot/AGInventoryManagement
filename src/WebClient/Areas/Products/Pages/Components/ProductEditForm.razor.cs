using AGInventoryManagement.WebClient.Areas.Products.Contracts;
using Microsoft.AspNetCore.Components;

namespace AGInventoryManagement.WebClient.Areas.Products.Pages.Components;

public partial class ProductEditForm
{
    [Parameter]
    public ProductVM Product { get; set; } = null!;

    [Parameter]
    public EventCallback<ProductVM> OnValidSubmit { get; set; }

    public async Task HandleValidSubmit()
    {
        await OnValidSubmit.InvokeAsync(Product);
    }
}
