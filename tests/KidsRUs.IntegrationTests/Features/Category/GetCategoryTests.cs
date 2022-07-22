using System.Net;
using ErrorResponse = KidsRUs.IntegrationTests.Models.ErrorResponse;

namespace KidsRUs.IntegrationTests.Features.Category;

public class GetCategoryTests : TestBase
{
    [Theory]
    [InlineData("Agrlprx5")]
    [InlineData("JwRPo2e7")]
    public async Task Category_WithExistence(string categorySku)
    {
        // Arrenge
        var client = Application.CreateClient();

        // Act
        var apiResponse = await client.GetFromJsonAsync<Models.ApiResponse<CategoryVm>>($"/api/v1/categories/{categorySku}");

        // Assert
        Assert.NotNull(apiResponse);
        var response = Assert.IsType<Models.ApiResponse<CategoryVm>>(apiResponse);
        Assert.NotNull(response.Data);
        Assert.Equal(response.Data.Sku, categorySku);
    }
    
    [Theory]
    [InlineData("43gsf43gs")]
    [InlineData("wwedsfd232")]
    public async Task Category_WithoutExistence(string categorySku)
    {
        // Arrenge
        var client = Application.CreateClient();

        // Act
        var response = await client.GetAsync($"/api/v1/categories/{categorySku}");

        // Assert
        Assert.False(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        
        var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        Assert.NotNull(errorResponse);
    }
}