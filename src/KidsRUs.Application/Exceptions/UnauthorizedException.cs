namespace KidsRUs.Application.Exceptions;

public class UnauthorizedException: CustomException
{
    public UnauthorizedException(
        string message, 
        List<string>? errors = default, 
        HttpStatusCode statusCode = HttpStatusCode.Unauthorized,
        ErrorStatus errorStatus = ErrorStatus.Unauthorized)
        : base(message, errors, statusCode, errorStatus)
    {
    }
}