using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KidsRUs.Api.Controllers.Base;

[ApiController]
public abstract class BaseApiController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}