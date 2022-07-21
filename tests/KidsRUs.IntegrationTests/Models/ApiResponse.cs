namespace KidsRUs.IntegrationTests.Models;

public class ApiResponse
{
    public string? ActionCode { get; set; }
}

public class ApiResponse<T> : ApiResponse
{
    public T Data { get; set; } = default!;
}