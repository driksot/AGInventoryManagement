using AGInventoryManagement.WebClient.Areas.Customers.Contracts;
using AGInventoryManagement.WebClient.Areas.Customers.Services;
using Microsoft.AspNetCore.Components;

namespace AGInventoryManagement.WebClient.Areas.Customers.Pages;

public partial class CustomerUpsert
{
    [Inject]
    public ICustomerService CustomerService { get; set; } = null!;

    private CustomerVM _customer = new();
    private string _title = "Add";

    protected override void OnInitialized()
    {
        _customer = new CustomerVM();
        _title = "Add";
    }

    private async Task HandleValidSubmit(CustomerVM customer)
    {
        Console.WriteLine("Customer: ", customer);
        await CustomerService.CreateCustomerAsync(customer);

        NavManager.NavigateTo("/customers");
    }
}
