
using AGInventoryManagement.Application.Common.Models;
using AGInventoryManagement.Application.Products.Commands.ArchiveProduct;
using AGInventoryManagement.Application.Products.Commands.CreateProduct;
using AGInventoryManagement.Application.Products.Commands.UpdateProduct;
using AGInventoryManagement.Application.Products.Queries.GetProduct;
using AGInventoryManagement.Application.Products.Queries.GetProductList;

namespace AGInventoryManagement.Web.Endpoints;

public class Products : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetProductList)
            .MapGet(GetProduct, "{id}")
            .MapPost(CreateProduct)
            .MapPut(UpdateProduct, "{id}")
            .MapPut(ArchiveProduct, "ArchiveProduct/{id}");
    }

    public Task<PaginatedList<ProductDto>> GetProductList(ISender sender, [AsParameters] GetProductListQuery query)
    {
        return sender.Send(query);
    }

    public Task<ProductDto> GetProduct(ISender sender, Guid id)
    {
        var query = new GetProductQuery(id);
        return sender.Send(query);
    }

    public Task<Guid> CreateProduct(ISender sender, CreateProductCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> UpdateProduct(ISender sender, Guid id, UpdateProductCommand command)
    {
        if (id != command.ProductId) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> ArchiveProduct(ISender sender, Guid id, ArchiveProductCommand command)
    {
        if (id != command.ProductId) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }
}
