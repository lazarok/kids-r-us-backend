using System.Net;
using KidsRUs.Application.Handlers.Tags.Commands.CreateTag;

namespace KidsRUs.IntegrationTests.Features.Tag;

public class AddTagToProductTests : TestBase
{
    [Theory]
    [InlineData("Agrlprx5")]
    [InlineData("JwRPo2e7")]
    public async Task Tag_IsCreated_WhenValidFieldsAreProvided_WithAdminUser(string productSku)
    {
        // Arrenge
        var (client, _) = await GetClientAsAdmin();
        var data = new CreateTagDto (){Name = "Tag1"};
        // Act
        var response = await client.PostAsJsonAsync($"/api/v1/tags/product/{productSku}", data);

        // Assert
        
        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        
        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<TagVm>>();
        Assert.NotNull(apiResponse);
        Assert.IsType<ApiResponse<TagVm>>(apiResponse);
        
        Assert.Equal(data.Name, apiResponse.Data.Name);
    }
    
    [Theory]
    [InlineData("43gsf43gs")]
    [InlineData("wwedsfd232")]
    public async Task Tag_IsNotCreated_WhenProductDontExist_WithAdminUser(string productSku)
    {
        // Arrenge
        var (client, _) = await GetClientAsAdmin();
        var data = new CreateTagDto (){Name = "Tag1"};
        // Act
        var response = await client.PostAsJsonAsync($"/api/v1/tags/product/{productSku}", data);

        // Assert
        
        Assert.False(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        
        var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        Assert.NotNull(errorResponse);
    }
}