using AGInventoryManagement.Domain.Common;

namespace AGInventoryManagement.Domain.Customers;

public class Customer : BaseAuditableEntity, ISoftDeletable
{
    private Customer(
        Guid id,
        string firstName,
        string lastName,
        string phoneNumber,
        string email)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Email = email;
    }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string PhoneNumber { get; private set; }

    public string Email { get; private set; }

    public bool IsDeleted { get; set; } = false;

    public DateTime? DeletedOnUtc { get; set; }

    public static DomainResult<Customer> Create(
        string firstName,
        string lastName,
        string phoneNumber,
        string email)
    {
        var customer = new Customer(
            Guid.NewGuid(),
            firstName,
            lastName,
            phoneNumber,
            email);

        return customer;
    }

#pragma warning disable CS8618
    private Customer() { } // EF
#pragma warning restore CS8618
}
