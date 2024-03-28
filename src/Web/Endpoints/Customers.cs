using AGInventoryManagement.Application.Common.Models;
using AGInventoryManagement.Application.Customers.Commands.CreateCustomer;
using AGInventoryManagement.Application.Customers.Commands.DeleteCustomer;
using AGInventoryManagement.Application.Customers.Queries.GetCustomer;
using AGInventoryManagement.Application.Customers.Queries.GetCustomerList;
using AGInventoryManagement.Application.Customers.Queries.GetCustomerListByName;

namespace AGInventoryManagement.Web.Endpoints;

public class Customers : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetCustomerList)
            .MapGet(GetCustomerListByName, "name/{customerName}")
            .MapGet(GetCustomer, "{id}")
            .MapPost(CreateCustomer)
            .MapDelete(DeleteCustomer, "{id}");
    }

    public async Task<PaginatedList<CustomerResponse>> GetCustomerList(
        ISender sender,
        [AsParameters] GetCustomerListQuery query)
    {
        var result = await sender.Send(query);

        return result.Value;
    }

    public async Task<List<CustomerResponse>> GetCustomerListByName(ISender sender, string customerName)
    {
        var query = new GetCustomerListByNameQuery(customerName);

        var result = await sender.Send(query);

        return result.Value;
    }

    public async Task<CustomerResponse> GetCustomer(ISender sender, Guid id)
    {
        var query = new GetCustomerQuery(id);

        var result = await sender.Send(query);

        return result.Value;
    }

    public async Task<Guid> CreateCustomer(ISender sender, CreateCustomerRequest request)
    {
        var command = new CreateCustomerCommand(
            request.FirstName,
            request.LastName,
            request.PhoneNumber,
            request.Email);

        var result = await sender.Send(command);

        return result.Value;
    }

    public async Task<IResult> DeleteCustomer(ISender sender, Guid id)
    {
        var command = new DeleteCustomerCommand(id);

        await sender.Send(command);

        return Results.NoContent();
    }
}
