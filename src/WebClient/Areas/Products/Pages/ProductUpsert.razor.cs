using AGInventoryManagement.WebClient.Areas.Products.Contracts;
using AGInventoryManagement.WebClient.Areas.Products.Services;
using Microsoft.AspNetCore.Components;

namespace AGInventoryManagement.WebClient.Areas.Products.Pages;
public partial class ProductUpsert
{
    [Parameter]
    public Guid Id { get; set; }

    [Inject]
    public IProductService ProductService { get; set; } = null!;

    private ProductVM _product = new();
    private string _title = "Add";

    protected override async Task OnInitializedAsync()
    {
        if (Id != Guid.Empty)
        {
            _title = "Edit";
            var productResponse = await ProductService.GetProductByIdAsync(Id);
            _product = new ProductVM
            {
                Id = productResponse.Id,
                Name = productResponse.Name,
                Description = productResponse.Description!,
                Price = productResponse.Price,
            };
        }
    }

    private async Task HandleValidSubmit(ProductVM product)
    {
        if (Id != Guid.Empty)
        {
            await ProductService.UpdateProductAsync(product);
        }
        else
        {
            await ProductService.CreateProductAsync(product);
        }

        NavManager.NavigateTo("/products");
    }
}
