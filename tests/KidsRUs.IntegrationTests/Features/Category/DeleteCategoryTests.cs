using System.Net;
using Bogus;

namespace KidsRUs.IntegrationTests.Features.Category;

public class DeleteCategoryTests : TestBase
{
    [Theory]
    [InlineData("Agrlprx5")]
    [InlineData("JwRPo2e7")]
    public async Task Category_IsDeleted_WhenValidFieldsAreProvided_WithAdminUser(string categorySku)
    {
        // Arrenge
        var (client, _) = await GetClientAsAdmin();

        // Act
        var response = await client.DeleteAsync($"/api/v1/categories/{categorySku}");

        // Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
    
    [Theory]
    [InlineData("43gsf43gs")]
    [InlineData("wwedsfd232")]
    public async Task Category_IsNotDeleted_WhenCategoryDontExist_WithAdminUser(string categorySku)
    {
        // Arrenge
        var (client, _) = await GetClientAsAdmin();

        // Act
        var response = await client.DeleteAsync($"/api/v1/categories/{categorySku}");

        // Assert
        
        Assert.False(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        
        var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        Assert.NotNull(errorResponse);
    }
    
    [Theory]
    [InlineData("Agrlprx5")]
    [InlineData("JwRPo2e7")]
    public async Task Category_IsNotDeleted_WhenValidFieldsAreProvided_WithAnonymUser(string categorySku)
    {
        // Arrenge
        var client = Application.CreateClient();

        // Act
        var response = await client.DeleteAsync($"/api/v1/categories/{categorySku}");

        // Assert
        Assert.False(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        
        var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        Assert.NotNull(errorResponse);
    }
}