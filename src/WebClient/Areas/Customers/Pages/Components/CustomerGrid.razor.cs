using AGInventoryManagement.WebClient.Areas.Common.Components;
using AGInventoryManagement.WebClient.Areas.Common.Contracts;
using AGInventoryManagement.WebClient.Areas.Products.Contracts;
using AGInventoryManagement.WebClient.Areas.Products;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using AGInventoryManagement.WebClient.Areas.Customers.Contracts;

namespace AGInventoryManagement.WebClient.Areas.Customers.Pages.Components;
public partial class CustomerGrid
{
    [CascadingParameter]
    public CustomerState State { get; set; } = null!;

    [Inject]
    IDialogService DialogService { get; set; } = null!;

    [Inject]
    ISnackbar Snackbar { get; set; } = null!;

    private IEnumerable<CustomerResponse> _pagedData = new List<CustomerResponse>();
    private MudTable<CustomerResponse> _table = new();
    private int _totalItems;
    private string? _searchString = null;

    private async Task<TableData<CustomerResponse>> ServerReload(TableState tableState)
    {
        Console.WriteLine("Page: " + tableState.Page);

        var pagingParams = new PaginationRequest(
            _searchString,
            tableState.SortLabel,
            tableState.SortDirection == SortDirection.Descending ? "desc" : "asc",
            tableState.Page = tableState.Page + 1,
            tableState.PageSize);

        await State.SetCustomersAsync(pagingParams);
        var data = State.Model!.Items.AsEnumerable();

        _totalItems = State.Model!.TotalCount;
        _pagedData = data;

        return new TableData<CustomerResponse>() { TotalItems = _totalItems, Items = _pagedData };
    }

    private void OnSearch(string text)
    {
        _searchString = text;
        _table.ReloadServerData();
    }

    private void HandleDelete(Guid customerId)
    {
        var parameters = new DialogParameters<ConfirmDialog>
        {
            { x => x.OnSubmit, new EventCallbackFactory().Create(this, new Action<Guid>(DeleteCustomer)) },
            { x => x.ContentText, "Do you really want to delete this customer record?" },
            { x => x.Id, customerId },
            { x => x.ButtonText, "Delete" },
            { x => x.Color, Color.Error }
        };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        DialogService.Show<ConfirmDialog>("Delete", parameters, options);
    }

    private async void DeleteCustomer(Guid customerId)
    {
        await State.RemoveCustomer(customerId);
        Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomEnd;
        Snackbar.Add("Product has been successfully deleted.", Severity.Normal);
        await _table.ReloadServerData();
    }
}
