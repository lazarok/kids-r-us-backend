namespace KidsRUs.Application.Models.Response;

public class ApiResponse
{
    public string? ActionCode { get; set; }
}

public class ApiResponse<T> : ApiResponse
{
    public T Data { get; set; }
}