namespace KidsRUs.Application.Exceptions;

public class NotAllowedException : CustomException
{
    public NotAllowedException(
        string message, 
        List<string>? errors = default, 
        HttpStatusCode statusCode = HttpStatusCode.Forbidden, 
        ErrorStatus errorStatus = ErrorStatus.Forbidden) 
        : base(message, errors, statusCode, errorStatus)
    {
    }
}