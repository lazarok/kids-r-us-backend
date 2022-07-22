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
    /// <summary>
    /// Sell product by product sku. Selling one at a time
    /// </summary>
    /// <param name="productSku"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [HttpPut("sell/{productSku}")]
    public async Task<IActionResult> SellProduct(string productSku)
    {
        await Mediator.Send(new CreateSaleCommand() {ProductSku = productSku});
        return NoContent();
    }
    
    /// <summary>
    /// Get products sold. It is possible to search by product name
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [ProducesResponseType(typeof(PaginationResponse<ProductVm>), StatusCodes.Status200OK)]
    [HttpGet("products-sold")]
    public async Task<IActionResult> GetProductsSold([FromQuery] GetProductsSoldQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
    
    /// <summary>
    /// Get gross profit. It is possible to request the gross profit in a given period of time,
    /// this takes the date of the sale of the products
    /// </summary>
    /// <param name="query">DateTime format UTC: 2022-07-20T06:31:27.140Z</param>
    /// <returns></returns>
    [ProducesResponseType(typeof(ApiResponse<decimal>), StatusCodes.Status200OK)]
    [HttpGet("gross-profit")]
    public async Task<IActionResult> GetGrossProfit([FromQuery] GetGrossProfitQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}