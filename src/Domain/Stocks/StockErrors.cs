using AGInventoryManagement.Domain.Common;

namespace AGInventoryManagement.Domain.Stocks;

public static class StockErrors
{
    public static Error NotFound = new(
        "Stock.NotFound",
        "The stock with the specified ID was not found.");

    public static Error Negative = new(
        "Stock.Negative",
        "The adjusted quantity cannot be negative.");
}
