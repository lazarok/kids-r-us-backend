using System.Net;

namespace KidsRUs.IntegrationTests.Features.Product;

public class GetProductTests : TestBase
{
    [Theory]
    [InlineData("Agrlprx5")]
    [InlineData("JwRPo2e7")]
    public async Task Product_WithExistence(string productSku)
    {
        // Arrenge
        var client = Application.CreateClient();

        // Act
        var apiResponse = await client.GetFromJsonAsync<ApiResponse<ProductVm>>($"/api/v1/products/{productSku}");

        // Assert
        Assert.NotNull(apiResponse);
        var response = Assert.IsType<ApiResponse<ProductVm>>(apiResponse);
        Assert.NotNull(response.Data);
        Assert.Equal(response.Data.Sku, productSku);
    }
    
    [Theory]
    [InlineData("43g4343sf4")]
    [InlineData("wwedsfd232")]
    public async Task Product_WithoutExistence(string productSku)
    {
        // Arrenge
        var client = Application.CreateClient();

        // Act
        var response = await client.GetAsync($"/api/v1/products/{productSku}");

        // Assert
        Assert.False(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        
        var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        Assert.NotNull(errorResponse);
    }
}