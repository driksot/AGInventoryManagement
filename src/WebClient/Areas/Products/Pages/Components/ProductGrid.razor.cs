using AGInventoryManagement.WebClient.Areas.Common.Components;
using AGInventoryManagement.WebClient.Areas.Common.Contracts;
using AGInventoryManagement.WebClient.Areas.Products.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AGInventoryManagement.WebClient.Areas.Products.Pages.Components;
public partial class ProductGrid
{
    [CascadingParameter]
    public ProductState State { get; set; } = null!;

    [Inject]
    IDialogService DialogService { get; set; } = null!;

    [Inject]
    ISnackbar Snackbar { get; set; } = null!;

    private IEnumerable<ProductResponse> _pagedData = new List<ProductResponse>();
    private MudTable<ProductResponse> _table = new();
    private int _totalItems;
    private string? _searchString = null;

    private async Task<TableData<ProductResponse>> ServerReload(TableState tableState)
    {
        Console.WriteLine("Page: " + tableState.Page);

        var pagingParams = new PaginationRequest(
            _searchString,
            tableState.SortLabel,
            tableState.SortDirection == SortDirection.Descending ? "desc" : "asc",
            tableState.Page = tableState.Page + 1,
            tableState.PageSize);

        await State.SetProductsAsync(pagingParams);
        var data = State.Model!.Items.AsEnumerable();

        _totalItems = State.Model!.TotalCount;
        _pagedData = data;

        return new TableData<ProductResponse>() { TotalItems = _totalItems, Items = _pagedData };
    }

    private void OnSearch(string text)
    {
        _searchString = text;
        _table.ReloadServerData();
    }

    private void HandleDelete(Guid productId)
    {
        var parameters = new DialogParameters<ConfirmDialog>
        {
            { x => x.OnSubmit, new EventCallbackFactory().Create(this, new Action<Guid>(DeleteProduct)) },
            { x => x.ContentText, "Do you really want to delete this product?" },
            { x => x.Id, productId },
            { x => x.ButtonText, "Delete" },
            { x => x.Color, Color.Error }
        };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        DialogService.Show<ConfirmDialog>("Delete", parameters, options);
    }

    private async void DeleteProduct(Guid productId)
    {
        await State.ProductService.DeleteProductAsync(productId);
        State.RemoveProduct(productId);
        Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomEnd;
        Snackbar.Add("Product has been successfully deleted.", Severity.Normal);
        await _table.ReloadServerData();
    }
}
