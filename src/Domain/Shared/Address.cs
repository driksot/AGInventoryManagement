namespace AGInventoryManagement.Domain.Shared;

public record Address(
    string Street,
    string? StreetExt,
    string City,
    string State,
    string Country,
    string PostalCode);
