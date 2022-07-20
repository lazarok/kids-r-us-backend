using AutoMapper;
using KidsRUs.Api.Controllers.Base;
using KidsRUs.Application.Handlers.Categories.Commands.CreateCategory;
using KidsRUs.Application.Handlers.Categories.Commands.DeleteCategory;
using KidsRUs.Application.Handlers.Categories.Commands.UpdateCategory;
using KidsRUs.Application.Handlers.Categories.Queries.GetAllCategory;
using KidsRUs.Application.Handlers.Categories.Queries.GetCategory;
using KidsRUs.Application.Models.Response;
using KidsRUs.Application.Models.ViewModels;
using KidsRUs.Application.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KidsRUs.Api.Controllers.v1;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/categories")]
public class CategoryController : BaseApiController
{
    private readonly IMapper _mapper;

    public CategoryController(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    [ProducesResponseType(typeof(ApiResponse<CategoryVm>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [HttpGet("{sku}")]
    public async Task<IActionResult> Get(string sku)
    {
        return Ok(await Mediator.Send(new GetCategoryQuery(sku)));
    }
    
    [ProducesResponseType(typeof(PaginationResponse<CategoryVm>), StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetAllCategoryQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [ProducesResponseType(typeof(ApiResponse<CategoryVm>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "Admin,Editor")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCategoryDto request)
    {
        var command = _mapper.Map<CreateCategoryCommand>(request);
        return StatusCode(StatusCodes.Status201Created, await Mediator.Send(command));
    }
    
    [ProducesResponseType(typeof(ApiResponse<CategoryVm>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "Admin,Editor")]
    [HttpPut("{sku}")]
    public async Task<IActionResult> Put(string sku, [FromBody] UpdateCategoryDto request)
    {
        return Ok(await Mediator.Send(new UpdateCategoryCommand()
        {
            Sku = sku,
            Name = request.Name
        }));
    }
    
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "Admin,Editor")]
    [HttpDelete("{sku}")]
    public async Task<IActionResult> Delete(string sku)
    {
        await Mediator.Send(new DeleteCategoryCommand {Sku = sku});
        return NoContent();
    }
}