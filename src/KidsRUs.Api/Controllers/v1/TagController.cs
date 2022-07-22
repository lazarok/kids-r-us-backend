using AutoMapper;
using KidsRUs.Api.Controllers.Base;
using KidsRUs.Application.Handlers.Tags.Commands.CreateTag;
using KidsRUs.Application.Handlers.Tags.Commands.DeleteTag;
using KidsRUs.Application.Models.Response;
using KidsRUs.Application.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KidsRUs.Api.Controllers.v1;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/tags")]
public class TagController : BaseApiController
{
    /// <summary>
    /// Add a new tag to existing product
    /// </summary>
    /// <param name="productSku"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [ProducesResponseType(typeof(ApiResponse<TagVm>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "Admin,Editor")]
    [HttpPost("product/{productSku}")]
    public async Task<IActionResult> Post(string productSku, [FromBody] CreateTagDto request)
    {
        var command = new CreateTagCommand
        {
            ProductSku = productSku,
            Name = request.Name
        };
        return StatusCode(StatusCodes.Status201Created, await Mediator.Send(command));
    }
    
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "Admin,Editor")]
    [HttpDelete("{sku}")]
    public async Task<IActionResult> Delete(string sku)
    {
        await Mediator.Send(new DeleteTagCommand {Sku = sku});
        return NoContent();
    }
}