using AGInventoryManagement.Domain.Common;

namespace AGInventoryManagement.Domain.Orders;

public static class OrderErrors
{
    public static Error NotFound = new(
        "Order.NotFound",
        "The order with the specified ID was not found.");

    public static Error InvalidType = new(
        "Order.InvalidType",
        "The provided order type is invalid.");

    public static Error InvalidStatus = new(
        "Order.InvalidStatus",
        "The provided order status is invalid.");

    public static Error LineItemNotFound = new(
        "Order.LineItemNotFound",
        "The line item with the specified ID was not found.");

    public static Error LineItemRequired = new(
        "Order.LineItemRequired",
        "The order must have at least one line item.");
}
