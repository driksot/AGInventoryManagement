using AGInventoryManagement.WebClient.Areas.Common.Contracts;
using AGInventoryManagement.WebClient.Areas.Products.Contracts;
using AGInventoryManagement.WebClient.Areas.Products.Services;
using Microsoft.AspNetCore.Components;

namespace AGInventoryManagement.WebClient.Areas.Products;
public partial class ProductState
{
    [Parameter]
    public RenderFragment ChildContent { get; set; } = null!;

    [Inject]
    public IProductService ProductService { get; set; } = null!;

    public PagingResponse<ProductResponse>? Model { get; set; }

    public PaginationRequest PaginationRequest { get; set; } = new(string.Empty, string.Empty, string.Empty, 1, 10);

    private ProductResponse? _selectedProduct;

    private ProductResponse? SelectedProduct
    {
        get { return _selectedProduct; }
        set
        {
            _selectedProduct = value;
            StateHasChanged();
        }
    }

    public bool Initialized { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Model = await ProductService.GetProductListAsync(PaginationRequest);
        SelectedProduct = Model.Items.First();
        Initialized = true;
    }

    public void SyncProduct()
    {
        var product = Model!.Items.First(p => p.Id == SelectedProduct!.Id);
        StateHasChanged();
    }

    public void RemoveProduct()
    {
        var product = Model!.Items.First(p => p.Id == SelectedProduct!.Id);
        Model!.Items.Remove(product);
        SelectedProduct = Model.Items.First();
        StateHasChanged();
    }

    public async Task SetProductsAsync(PaginationRequest request)
    {
        PaginationRequest = request;
        Model = await ProductService.GetProductListAsync(PaginationRequest);
    }
}
