namespace AGInventoryManagement.WebClient.Areas.Common.Contracts;

public record PaginationRequest(
    string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageSize);
