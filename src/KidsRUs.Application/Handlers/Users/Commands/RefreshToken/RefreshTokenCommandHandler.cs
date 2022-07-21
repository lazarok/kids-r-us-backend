using System.Security.Claims;
using KidsRUs.Application.Extensions;
using KidsRUs.Application.Models.Dtos;
using KidsRUs.Application.Services;
using Microsoft.Extensions.Configuration;

namespace KidsRUs.Application.Handlers.Users.Commands.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, ApiResponse<TokenDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;

    public RefreshTokenCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _configuration = configuration;
    }

    public async Task<ApiResponse<TokenDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var userId = _tokenService.GetClaims(request.AccessToken).GetClaimValue<int>(ClaimTypes.NameIdentifier);
        var user = await _unitOfWork.User.GetByIdAsync(userId);

        if (string.IsNullOrEmpty(user.RefreshToken) || user.RefreshToken != request.RefreshToken)
        {
            throw new CustomException("Invalid refresh token!", statusCode: HttpStatusCode.Unauthorized, errorStatus: ErrorStatus.Unauthorized);
        }

        if (user.RefreshTokenExpires == null || user.RefreshTokenExpires <= DateTime.UtcNow)
        {
            throw new CustomException("Refresh token expired!", statusCode: HttpStatusCode.Unauthorized, errorStatus: ErrorStatus.Unauthorized);
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