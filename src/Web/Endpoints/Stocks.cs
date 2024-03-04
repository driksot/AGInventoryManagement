
using AGInventoryManagement.Application.Common.Models;
using AGInventoryManagement.Application.Products.Queries.GetProductList;
using AGInventoryManagement.Application.Stocks.Commands.UpdateStockIdeal;
using AGInventoryManagement.Application.Stocks.Commands.UpdateStockOnHand;
using AGInventoryManagement.Application.Stocks.Queries.GetLowStock;
using AGInventoryManagement.Application.Stocks.Queries.GetSnapshotList;

namespace AGInventoryManagement.Web.Endpoints;

public class Stocks : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetLowStock, "LowStock")
            .MapGet(GetSnapshotHistory, "Snapshots")
            .MapPut(UpdateStockIdeal, "AdjustIdeal/{id}")
            .MapPut(UpdateStockOnHand, "AdjustOnHand/{id}");
    }

    public Task<PaginatedList<ProductDto>> GetLowStock(ISender sender, [AsParameters] GetLowStockQuery query)
    {
        return sender.Send(query);
    }

    public Task<PaginatedList<SnapshotDto>> GetSnapshotHistory(ISender sender, [AsParameters] GetSnapshotListQuery query)
    {
        return sender.Send(query);
    }

    public async Task<IResult> UpdateStockIdeal(ISender sender, Guid id, UpdateStockIdealCommand command)
    {
        if (id != command.ProductId) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> UpdateStockOnHand(ISender sender, Guid id, UpdateStockOnHandCommand command)
    {
        if (id != command.ProductId) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }
}
