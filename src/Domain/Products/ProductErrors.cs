using AGInventoryManagement.Domain.Common;

namespace AGInventoryManagement.Domain.Products;

public static class ProductErrors
{
    public static Error NotFound = new(
        "Product.NotFound",
        "The product with the specified ID was not found.");

    public static Error InvalidSku = new(
        "Product.InvalidSku",
        "The provided product sku is not a valid format.");

    public static Error MissingStock = new(
        "Product.MissingStock",
        "The product's stock was not created.");
}
