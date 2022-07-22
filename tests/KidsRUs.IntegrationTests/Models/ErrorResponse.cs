namespace KidsRUs.IntegrationTests.Models;

public class ErrorResponse
{
    public string? Status { get; set; }
    public string Message { get; set; }
    public List<string>? ErrorMessages { get; set; }
}