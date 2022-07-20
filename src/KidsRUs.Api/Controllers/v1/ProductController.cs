using AutoMapper;
using KidsRUs.Api.Controllers.Base;
using KidsRUs.Application.Handlers.Products.Commands.CreateProduct;
using KidsRUs.Application.Handlers.Products.Commands.DeleteProduct;
using KidsRUs.Application.Handlers.Products.Commands.UpdateProduct;
using KidsRUs.Application.Handlers.Products.Queries.GetAllProduct;
using KidsRUs.Application.Handlers.Products.Queries.GetCountProduct;
using KidsRUs.Application.Handlers.Products.Queries.GetProduct;
using KidsRUs.Application.Models.Response;
using KidsRUs.Application.Models.ViewModels;
using KidsRUs.Application.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KidsRUs.Api.Controllers.v1;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/products")]
public class ProductController : BaseApiController
{
    private readonly IMapper _mapper;

    public ProductController(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    [ProducesResponseType(typeof(ApiResponse<ProductVm>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ProductVm>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [HttpGet("{sku}")]
    public async Task<IActionResult> Get(string sku)
    {
        return Ok(await Mediator.Send(new GetProductQuery(sku)));
    }
    
    [ProducesResponseType(typeof(PaginationResponse<ProductVm>), StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetAllProductQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
    
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    [HttpGet("count")]
    public async Task<IActionResult> GetCount([FromQuery] GetCountProductQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [ProducesResponseType(typeof(ApiResponse<ProductVm>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "Admin,Editor")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateProductDto request)
    {
        var command = _mapper.Map<CreateProductCommand>(request);
        return StatusCode(StatusCodes.Status201Created, await Mediator.Send(command));
    }
    
    [ProducesResponseType(typeof(ApiResponse<ProductVm>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "Admin,Editor")]
    [HttpPut("{sku}")]
    public async Task<IActionResult> Put(string sku, [FromBody] UpdateProductDto request)
    {
        return Ok(await Mediator.Send(new UpdateProductCommand
        {
            Sku = sku,
            Name = request.Name,
            Description = request.Description,
            ProductStock = request.ProductStock,
            Price = request.Price,
            AverageRating = request.AverageRating,
            Info = request.Info
        }));
    }
    
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
    //[Authorize(Roles = "Admin,Editor")]
    [HttpDelete("{sku}")]
    public async Task<IActionResult> Delete(string sku)
    {
        await Mediator.Send(new DeleteProductCommand {Sku = sku});
        return NoContent();
    }
}