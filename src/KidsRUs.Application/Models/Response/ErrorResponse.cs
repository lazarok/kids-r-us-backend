namespace KidsRUs.Application.Models.Response
{
    public class ErrorResponse    
    {
        public string? Status { get; set; }
        public string Message { get; set; }
        public List<string>? ErrorMessages { get; set; }
        
        public ErrorResponse(ErrorStatus? status = null, string message = null, List<string>? errorMessages = null)
        {
            Status = status?.ToString();
            Message = message;
            ErrorMessages = errorMessages;
        }
        
        public void SetError(ErrorStatus status, string message)
        {
            Status = status.ToString();
            Message = message;
        }
        
        public void SetError(string message, List<string>? errorMessages, ErrorStatus status)
        {
            Message = message;
            ErrorMessages = errorMessages;
            Status = status.ToString();
        }
        
        public void SetError(ErrorStatus status)
        {
            SetError(status, status.ToString());
        }
    
        public void SetError(string message)
        {
            Message = message;
        }
        
        public void SetError(Exception exception)
        {
            SetError(ErrorStatus.Unhandled, exception.Message);
        }

        public override string ToString()
        {
            return !string.IsNullOrEmpty(Message) ? Message : Status;
        }
    }
}