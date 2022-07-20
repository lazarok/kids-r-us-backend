using KidsRUs.Api.Controllers.Base;
using KidsRUs.Application.Handlers.Products.Queries.GetAllProduct;
using KidsRUs.Application.Handlers.Products.Queries.GetProduct;
using KidsRUs.Application.Handlers.Sales.Commands.CreateSale;
using KidsRUs.Application.Handlers.Sales.Queries.GetGrossProfit;
using KidsRUs.Application.Handlers.Sales.Queries.GetProductsSold;
using KidsRUs.Application.Models.Response;
using KidsRUs.Application.Models.ViewModels;
using KidsRUs.Application.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace KidsRUs.Api.Controllers.v1;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/sales")]
public class SaleController : BaseApiController
{
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [HttpPut("sell/{productSku}")]
    public async Task<IActionResult> SellProduct(string productSku)
    {
        await Mediator.Send(new CreateSaleCommand() {ProductSku = productSku});
        return NoContent();
    }
    
    [ProducesResponseType(typeof(PaginationResponse<ProductVm>), StatusCodes.Status200OK)]
    [HttpGet("products-sold")]
    public async Task<IActionResult> GetProductsSold([FromQuery] GetProductsSoldQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
    
    /// <summary>
    /// DateTime format UTC: 2022-07-20T06:31:27.140Z
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [ProducesResponseType(typeof(ApiResponse<decimal>), StatusCodes.Status200OK)]
    [HttpGet("gross-profit")]
    public async Task<IActionResult> GetGrossProfit([FromQuery] GetGrossProfitQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}