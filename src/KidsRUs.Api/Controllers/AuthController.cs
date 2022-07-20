using AutoMapper;
using KidsRUs.Api.Controllers.Base;
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
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] SignInDto request)
    {
        var command = _mapper.Map<SignInCommand>(request);
        return Ok(await Mediator.Send(command));
    }
}