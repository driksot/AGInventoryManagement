﻿using AGInventoryManagement.Domain.Common;

namespace AGInventoryManagement.Domain.Customers;

public class Customer : BaseAuditableEntity
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
}
