using KidsRUs.Application.Handlers.Users.Commands.SignIn;
using KidsRUs.Application.Models.Dtos;

namespace KidsRUs.Application.Handlers.Users.Commands.RefreshToken;

public class RefreshTokenCommand : IRequest<ApiResponse<TokenDto>>
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}