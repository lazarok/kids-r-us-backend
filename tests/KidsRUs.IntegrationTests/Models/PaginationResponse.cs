namespace KidsRUs.IntegrationTests.Models;

public class PaginationResponse<T> : ApiResponse<List<T>>
{
    public int CurrentPage { get; set; }

    public int TotalPages { get; set; }

    public int TotalCount { get; set; }

    public int PageSize { get; set; }

    public bool HasPreviousPage { get; set; }

    public bool HasNextPage { get; set; }
}