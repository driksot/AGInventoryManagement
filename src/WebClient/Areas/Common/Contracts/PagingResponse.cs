﻿namespace AGInventoryManagement.WebClient.Areas.Common.Contracts;

public class PagingResponse<T>
{
    public List<T> Items { get; set; } = [];

    public int Page { get; set; }

    public int PageSize { get; set; }

    public int TotalCount { get; set; }

    public bool HasNextPage { get; set; }

    public bool HasPreviousPage { get; set; }
}
