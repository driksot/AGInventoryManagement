using AGInventoryManagement.WebClient.Areas.Common.Contracts;
using AGInventoryManagement.WebClient.Areas.Customers.Contracts;
using AGInventoryManagement.WebClient.Areas.Customers.Services;
using Microsoft.AspNetCore.Components;

namespace AGInventoryManagement.WebClient.Areas.Customers;
public partial class CustomerState
{
    [Parameter]
    public RenderFragment ChildContent { get; set; } = null!;

    [Inject]
    public ICustomerService CustomerService { get; set; } = null!;

    public PagingResponse<CustomerResponse>? Model { get; set; }

    public PaginationRequest PaginationRequest { get; set; } = new(string.Empty, string.Empty, string.Empty, 1, 10);

    private CustomerResponse? _selectedCustomer;

    public CustomerResponse? SelectedCustomer
    {
        get { return _selectedCustomer; }
        set
        {
            _selectedCustomer = value;
            StateHasChanged();
        }
    }

    public bool Initialized { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Model = await CustomerService.GetCustomerListAsync(PaginationRequest);
    }

    public void SyncCustomer(Guid id)
    {
        SelectedCustomer = Model!.Items.First(c => c.Id == id);
        StateHasChanged();
    }

    public async Task RemoveCustomer(Guid id)
    {
        var customer = Model!.Items.First(c => c.Id == id);
        Model!.Items.Remove(customer);
        await CustomerService.DeleteCustomerAsync(customer.Id);
        StateHasChanged();
    }

    public async Task SetCustomersAsync(PaginationRequest request)
    {
        PaginationRequest = request;
        Model = await CustomerService.GetCustomerListAsync(PaginationRequest);
    }
}
