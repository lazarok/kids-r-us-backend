using System.Security.Claims;
using KidsRUs.Api.Controllers.Base;
using KidsRUs.Application.Exceptions;
using KidsRUs.Application.Extensions;
using KidsRUs.Application.Handlers.Products.Commands.AddImage;
using KidsRUs.Application.Handlers.Products.Commands.DeleteImage;
using KidsRUs.Application.Helper;
using KidsRUs.Application.Models.Dtos;
using KidsRUs.Application.Models.Response;
using KidsRUs.Application.Repositories;
using KidsRUs.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace KidsRUs.Api.Controllers.v1;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/mediafiles")]
public class MediaFileController : BaseApiController
{
    private readonly IProductPictureService _productPictureService;

    public MediaFileController(IProductPictureService productPictureService)
    {
        _productPictureService = productPictureService;
    }

    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
    [HttpPost("upload/product/{productSku}")]
    public async Task<ActionResult> UploadProduct([FromRoute] string productSku, IFormFile file)
    {
        await using var stream = file.OpenReadStream();
        
        var command = new AddImageCommand()
        {
            ProductSku = productSku,
            MediaFile = new MediaFileDto()
            {
                FileName = file.FileName,
                FileContent = stream
            }
        };
        
        return StatusCode(StatusCodes.Status201Created, await Mediator.Send(command));
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("preview/{containerBase64}/{filenameBase64}")]
    public async Task<ActionResult> Preview(string containerBase64, string filenameBase64)
    {
        var mediaFile = await _productPictureService.GetPictureAsync(containerBase64.DecodeFromBase64(), filenameBase64.DecodeFromBase64());

        if (mediaFile == null)
        {
            return NotFound();
        }
        
        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(mediaFile.FileName, out var contentType))
        {
            contentType = "application/octet-stream";
        }
            
        return File(mediaFile.FileContent, contentType);
    }
    
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "Admin,Editor")]
    [HttpDelete("{imageSku}")]
    public async Task<IActionResult> Delete(string imageSku)
    {
        await Mediator.Send(new DeleteImageCommand {Sku = imageSku});
        return NoContent();
    }
}