namespace KidsRUs.Application.Exceptions;

public class CustomException : Exception
{
    public List<string>? ErrorMessages { get; protected set; }

    public HttpStatusCode StatusCode { get; }
    
    public ErrorStatus ErrorStatus { get; }

    public CustomException(
        string message, 
        List<string>? errors = default, 
        HttpStatusCode statusCode = HttpStatusCode.BadRequest, 
        ErrorStatus errorStatus = ErrorStatus.Unhandled)
        : base(message)
    {
        ErrorMessages = errors;
        StatusCode = statusCode;
        ErrorStatus = errorStatus;
    }
}