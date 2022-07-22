using System.Net;
using Bogus;
using KidsRUs.Application.Handlers.Categories.Commands.UpdateCategory;

namespace KidsRUs.IntegrationTests.Features.Category;

public class UpdateCategoryTests : TestBase
{
    [Theory]
    [InlineData("Agrlprx5")]
    [InlineData("JwRPo2e7")]
    public async Task Category_IsUpdated_WhenValidFieldsAreProvided_WithAdminUser(string categorySku)
    {
        // Arrenge
        var (client, _) = await GetClientAsAdmin();
        var data = new Faker<UpdateCategoryDto>()
            .RuleFor(_ => _.Name, f => $"{f.Lorem.Word()}-{Guid.NewGuid()}")
            .Generate();

        // Act
        var response = await client.PutAsJsonAsync($"/api/v1/categories/{categorySku}", data);

        // Assert
        
        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<CategoryVm>>();
        Assert.NotNull(apiResponse);
        Assert.NotNull(apiResponse.Data);
        Assert.Equal(data.Name, apiResponse.Data.Name);
        
        var newCategory = await client.GetFromJsonAsync<ApiResponse<CategoryVm>>($"/api/v1/categories/{apiResponse.Data.Sku}");
        Assert.NotNull(newCategory);
        Assert.NotNull(newCategory.Data);
        Assert.Equal(newCategory.Data.Sku, apiResponse.Data.Sku);
    }
    
    [Theory]
    [InlineData("43gsf43gs")]
    [InlineData("wwedsfd232")]
    public async Task Category_IsNotUpdated_WhenCategoryDontExist_WithAdminUser(string categorySku)
    {
        // Arrenge
        var (client, _) = await GetClientAsAdmin();

        var data = new Faker<UpdateCategoryDto>()
            .RuleFor(_ => _.Name, f => $"{f.Lorem.Word()}-{Guid.NewGuid()}")
            .Generate();

        // Act
        var response = await client.PutAsJsonAsync($"/api/v1/categories/{categorySku}", data);

        // Assert
        
        Assert.False(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        
        var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        Assert.NotNull(errorResponse);
    }
    
    [Theory]
    [InlineData("Agrlprx5")]
    [InlineData("JwRPo2e7")]
    public async Task Category_IsNotUpdated_WhenValidFieldsAreProvided_WithAnonymUser(string categorySku)
    {
        // Arrenge
        var client = Application.CreateClient();
        var data = new Faker<UpdateCategoryDto>()
            .RuleFor(_ => _.Name, f => $"{f.Lorem.Word()}-{Guid.NewGuid()}")
            .Generate();

        // Act
        var response = await client.PutAsJsonAsync($"/api/v1/categories/{categorySku}", data);

        // Assert
        
        Assert.False(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        
        var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        Assert.NotNull(errorResponse);
    }
    
}