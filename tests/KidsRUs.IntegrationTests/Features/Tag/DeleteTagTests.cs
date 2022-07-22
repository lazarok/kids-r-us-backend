using System.Net;

namespace KidsRUs.IntegrationTests.Features.Tag;

public class DeleteTagTests : TestBase
{
    [Theory]
    [InlineData("Agrlprx5")]
    [InlineData("JwRPo2e7")]
    public async Task Tag_IsDeleted_WhenValidFieldsAreProvided_WithAdminUser(string tagSku)
    {
        // Arrenge
        var (client, _) = await GetClientAsAdmin();

        // Act
        var response = await client.DeleteAsync($"/api/v1/tags/{tagSku}");

        // Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
    
    [Theory]
    [InlineData("43gsf43gs")]
    [InlineData("wwedsfd232")]
    public async Task Tag_IsNotDeleted_WhenTagDontExist_WithAdminUser(string tagSku)
    {
        // Arrenge
        var (client, _) = await GetClientAsAdmin();

        // Act
        var response = await client.DeleteAsync($"/api/v1/tags/{tagSku}");

        // Assert
        
        Assert.False(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        
        var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        Assert.NotNull(errorResponse);
    }
}