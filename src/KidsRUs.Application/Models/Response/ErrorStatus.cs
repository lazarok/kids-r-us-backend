namespace KidsRUs.Application.Models.Response;

public enum ErrorStatus
{
    Unhandled,
    Unauthorized,
    Forbidden,
    Validation,
    RefreshTokenExpired,
    InvalidRefreshToken,
    NotFound
}