using AGInventoryManagement.WebClient.Areas.Common.Contracts;
using AGInventoryManagement.WebClient.Areas.Products.Contracts;

namespace AGInventoryManagement.WebClient.Areas.Products.Services;

public interface IProductService
{
    Task<ProductResponse> GetProductByIdAsync(Guid productId);
    Task<PagingResponse<ProductResponse>> GetProductListAsync(PaginationRequest pagination);
    Task CreateProductAsync(ProductVM product);
    Task UpdateProductAsync(ProductVM product);
    Task DeleteProductAsync(Guid productId);
}
