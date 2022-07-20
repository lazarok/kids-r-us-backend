using KidsRUs.Application.Models.Dtos;

namespace KidsRUs.Application.Handlers.Users.Commands.SignIn;

public class SignInCommand : IRequest<ApiResponse<TokenDto>>, IMapFrom<User>
{
    public string Email { get; set; }
    public string Password { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SignInDto, SignInCommand>();
    }
}