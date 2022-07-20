using System.Security.Claims;
using KidsRUs.Application.Models.Dtos;
using KidsRUs.Application.Services;
using Microsoft.Extensions.Configuration;

namespace KidsRUs.Application.Handlers.Users.Commands.SignIn;

public class SignInCommandHandler : IRequestHandler<SignInCommand, ApiResponse<TokenDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public SignInCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService,IMapper mapper, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<ApiResponse<TokenDto>> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.User.FindByEmailAsync(request.Email);

        if (user == default)
        {
            throw new NotFoundException("User", request.Email);
        }

        if (!PasswordHelper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            throw new CustomException("Invalid password", statusCode: HttpStatusCode.Unauthorized, errorStatus: ErrorStatus.Unauthorized);
        }
        
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Role, user.Role.Name)
        };
        
        var response = new ApiResponse<TokenDto>
        {
            Data = _tokenService.BuildToken(claims)
        };
        
        user.RefreshToken = response.Data.RefreshToken;
        user.RefreshTokenExpires = DateTime.UtcNow.AddMonths(int.Parse(_configuration["Jwt:RefreshTokenExpires"]));

        await _unitOfWork.SaveAsync(cancellationToken);

        return response;
    }
}