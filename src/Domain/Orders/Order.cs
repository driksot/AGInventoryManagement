using AGInventoryManagement.Domain.Common;
using AGInventoryManagement.Domain.Shared;

namespace AGInventoryManagement.Domain.Orders;

public class Order : BaseAuditableEntity
{
    private Order(
        Guid id,
        string orderNumber,
        Guid customerId,
        OrderType type,
        OrderStatus status,
        Address orderAddress)
        : base(id)
    {
        OrderNumber = orderNumber;
        CustomerId = customerId;
        Type = type;
        Status = status;
        OrderAddress = orderAddress;
        OrderDate = DateTime.Now;
    }

    public string OrderNumber { get; set; }

    public Guid CustomerId { get; set; }

    public OrderType Type { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Generated;

    public DateTime OrderDate { get; set; }

    public Address? OrderAddress { get; set; }


    private readonly List<LineItem> _lineItems = [];

    public IReadOnlyList<LineItem> LineItems => [.. _lineItems];

    public static DomainResult<Order> Create(
        Guid customerId,
        string type,
        string street,
        string? streetExt,
        string city,
        string state,
        string country,
        string postalCode)
    {
        var orderNumber = "To be implemented";

        // Parse type string to OrderType enum and validate
        if (!OrderType.TryFromName(type, out var orderType))
        {
            return DomainResult.Failure<Order>(OrderErrors.InvalidType);
        }

        var order = new Order(
            Guid.NewGuid(),
            orderNumber,
            customerId,
            orderType,
            OrderStatus.Generated,
            new Address(
                street,
                streetExt,
                city,
                state,
                country,
                postalCode));

        return order;
    }

#pragma warning disable CS8618
    private Order() { } // EF
#pragma warning restore CS8618
}
