using AutoMapper;
using KidsRUs.Api.Controllers.Base;
using KidsRUs.Application.Handlers.Users.Commands.RefreshToken;
using KidsRUs.Application.Handlers.Users.Commands.SignIn;
using KidsRUs.Application.Models.Dtos;
using KidsRUs.Application.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace KidsRUs.Api.Controllers;

[Route("api/auth")]
public class AuthController : BaseApiController
{
    private readonly IMapper _mapper;

    public AuthController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [ProducesResponseType(typeof(ApiResponse<TokenDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] SignInDto request)
    {
        var command = _mapper.Map<SignInCommand>(request);
        return Ok(await Mediator.Send(command));
    }
    
    [ProducesResponseType(typeof(ApiResponse<TokenDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] TokenDto request)
    {
        var command = new RefreshTokenCommand
        {
            AccessToken = request.AccessToken,
            RefreshToken = request.RefreshToken
        };
        
        return Ok(await Mediator.Send(command));
    }
}