using AGInventoryManagement.WebClient.Areas.Common.Contracts;
using AGInventoryManagement.WebClient.Areas.Products.Contracts;

namespace AGInventoryManagement.WebClient.Areas.Products.Services;

public interface IProductService
{
    Task<PagingResponse<ProductResponse>> GetProductListAsync(PaginationRequest pagination);
}
