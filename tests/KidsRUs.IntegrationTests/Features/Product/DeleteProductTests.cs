using System.Net;

namespace KidsRUs.IntegrationTests.Features.Product;

public class DeleteProductTests : TestBase
{
    [Theory]
    [InlineData("Agrlprx5")]
    [InlineData("JwRPo2e7")]
    public async Task Product_IsDeleted_WhenValidFieldsAreProvided_WithAdminUser(string productSku)
    {
        // Arrenge
        var (client, _) = await GetClientAsAdmin();

        // Act
        var response = await client.DeleteAsync($"/api/v1/products/{productSku}");

        // Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
    
    [Theory]
    [InlineData("43gsf43gs")]
    [InlineData("wwedsfd232")]
    public async Task Product_IsNotDeleted_WhenProductDontExist_WithAdminUser(string productSku)
    {
        // Arrenge
        var (client, _) = await GetClientAsAdmin();

        // Act
        var response = await client.DeleteAsync($"/api/v1/products/{productSku}");

        // Assert
        
        Assert.False(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        
        var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        Assert.NotNull(errorResponse);
    }
    
    [Theory]
    [InlineData("Agrlprx5")]
    [InlineData("JwRPo2e7")]
    public async Task Product_IsNotDeleted_WhenValidFieldsAreProvided_WithAnonymUser(string productSku)
    {
        // Arrenge
        var client = Application.CreateClient();

        // Act
        var response = await client.DeleteAsync($"/api/v1/products/{productSku}");

        // Assert
        Assert.False(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        
        var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        Assert.NotNull(errorResponse);
    }
}