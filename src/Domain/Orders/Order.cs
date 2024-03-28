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

    public string OrderNumber { get; private set; }

    public Guid CustomerId { get; private set; }

    public OrderType Type { get; private set; }

    public OrderStatus Status { get; private set; } = OrderStatus.Generated;

    public DateTime OrderDate { get; private set; }

    public Address? OrderAddress { get; private set; }


    private readonly List<LineItem> _lineItems = [];

    public IReadOnlyList<LineItem> LineItems => [.. _lineItems];

    public static DomainResult<Order> Create(
        Guid customerId,
        int orderCount,
        string type,
        string street,
        string? streetExt,
        string city,
        string state,
        string country,
        string postalCode)
    {
        // Parse type string to OrderType enum and validate
        if (!OrderType.TryFromName(type, out var orderType))
        {
            return DomainResult.Failure<Order>(OrderErrors.InvalidType);
        }

        var orderNumber = GenerateOrderNumber(orderCount + 1, orderType);

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

    public DomainResult AddLineItem(Guid productId, decimal price)
    {
        var lineItem = new LineItem(
            Guid.NewGuid(),
            Id,
            productId,
            price);

        if (Status == OrderStatus.Generated)
        {
            Status = OrderStatus.InProgress;
        }

        _lineItems.Add(lineItem);

        return DomainResult.Success();
    }

    public DomainResult RemoveLineItem(Guid lineItemId)
    {
        if (HasOneLineItem())
        {
            return DomainResult.Failure(OrderErrors.LineItemRequired);
        }

        var lineItem = _lineItems.FirstOrDefault(li => li.Id == lineItemId);

        if (lineItem is null)
        {
            return DomainResult.Failure(OrderErrors.LineItemNotFound);
        }

        _lineItems.Remove(lineItem);

        return DomainResult.Success();
    }

    private static string GenerateOrderNumber(int orderCount, OrderType type)
    {
        var prefix = string.Empty;

        string sequenceFormat = "00000000.##";

        if (type == OrderType.Purchase)
        {
            prefix = "PO";
        }

        if (type == OrderType.Sales)
        {
            prefix = "SO";
        }

        var sequenceString = orderCount.ToString(sequenceFormat);

        var orderNumber = string.Join("-", prefix, sequenceString);

        return orderNumber;
    }

    private bool HasOneLineItem() => _lineItems.Count == 1;

#pragma warning disable CS8618
    private Order() { } // EF
#pragma warning restore CS8618
}
