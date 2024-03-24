
using AGInventoryManagement.Application.Common.Models;
using AGInventoryManagement.Application.Products.Commands.CreateProduct;
using AGInventoryManagement.Application.Products.Commands.DeleteProduct;
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
            .MapDelete(DeleteProduct, "{id}");
    }

    public async Task<PaginatedList<ProductDto>> GetProductList(ISender sender, [AsParameters] GetProductListQuery query)
    {
        var result = await sender.Send(query);

        return result.Value;
    }

    public async Task<ProductDto> GetProduct(ISender sender, Guid id)
    {
        var query = new GetProductQuery(id);

        var result = await sender.Send(query);

        return result.Value;
    }

    public async Task<Guid> CreateProduct(ISender sender, CreateProductCommand command)
    {
        var result = await sender.Send(command);

        return result.Value;
    }

    public async Task<IResult> UpdateProduct(ISender sender, Guid id, UpdateProductCommand command)
    {
        if (id != command.ProductId) return Results.BadRequest();

        await sender.Send(command);

        return Results.NoContent();
    }

    public async Task<IResult> DeleteProduct(ISender sender, Guid id)
    {
        var command = new DeleteProductCommand(id);

        await sender.Send(command);

        return Results.NoContent();
    }
}
