using System.Net;
using KidsRUs.Application.Models.Response;
using ErrorResponse = KidsRUs.IntegrationTests.Models.ErrorResponse;

namespace KidsRUs.IntegrationTests.Features.Sale;

public class SellProductTests : TestBase
{
    [Theory]
    [InlineData("Agrlprx5")]
    [InlineData("JwRPo2e7")]
    public async Task Product_IsSold_WhenValidFieldsAreProvided(string productSku)
    {
        // Arrenge
        var client = Application.CreateClient();
        var apiResponse = await client.GetFromJsonAsync<Models.ApiResponse<ProductVm>>($"/api/v1/products/{productSku}");
        var stock = apiResponse.Data.ProductStock;

        // Act
        var response = await client.PutAsJsonAsync($"/api/v1/sales/sell/{productSku}", new { });

        // Assert
        
        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        
        apiResponse = await client.GetFromJsonAsync<Models.ApiResponse<ProductVm>>($"/api/v1/products/{productSku}");
        Assert.Equal(stock - 1, apiResponse.Data.ProductStock);
    }
    
    [Theory]
    [InlineData("oWRgj2Bp")] // productId: 14
    [InlineData("KprwL1W9")] // productId: 15
    public async Task Product_IsNotSold_WhenProductNotHaveStock(string productSku)
    {
        // Arrenge
        var client = Application.CreateClient();

        // Act
        var response = await client.PutAsJsonAsync($"/api/v1/sales/sell/{productSku}", new { });

        // Assert
        Assert.False(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        
        var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        Assert.NotNull(errorResponse);
        Assert.Equal(ErrorStatus.Validation.ToString(), errorResponse.Status);
        Assert.Equal($"Product '{productSku}' has no stock", errorResponse.Message);
    }
    
    [Theory]
    [InlineData("43g4343sf4")]
    [InlineData("wwedsfd232")]
    public async Task Product_IsNotSold_WhenProductDontExist(string productSku)
    {
        // Arrenge
        var client = Application.CreateClient();

        // Act
        var response = await client.PutAsJsonAsync($"/api/v1/sales/sell/{productSku}", new { });

        // Assert
        Assert.False(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        
        var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        Assert.NotNull(errorResponse);
    }
}