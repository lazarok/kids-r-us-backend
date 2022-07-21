using System.Net;
using Bogus;
using KidsRUs.Application.Handlers.Categories.Commands.CreateCategory;
using KidsRUs.Application.Models.ViewModels;

namespace KidsRUs.IntegrationTests.Features.Category;

public class CreateCategoryTests : TestBase
{
    [Fact]
    public async Task Category_IsCreated_WhenValidFieldsAreProvided_WithAdminUser()
    {
        // Arrenge
        var (client, _) = await GetClientAsAdmin();
        var data = new Faker<CreateCategoryDto>()
            .RuleFor(_ => _.Name, f => $"{f.Lorem.Word()}-{Guid.NewGuid()}")
            .Generate();

        // Act
        var postPesponse = await client.PostAsJsonAsync("/api/v1/categories", data);
        postPesponse.EnsureSuccessStatusCode();

        // Assert
        
        Assert.True(postPesponse.IsSuccessStatusCode);
        
        var apiResponse = await postPesponse.Content.ReadFromJsonAsync<ApiResponse<CategoryVm>>();
        Assert.NotNull(apiResponse);
        Assert.IsType<ApiResponse<CategoryVm>>(apiResponse);
    }
    
    [Fact]
    public async Task Category_IsNotCreated_WhenValidFieldsAreProvided_WithAnonymUser()
    {
        // Arrenge
        var client = Application.CreateClient();
        var data = new Faker<CreateCategoryDto>()
            .RuleFor(_ => _.Name, f => $"{f.Lorem.Word()}-{Guid.NewGuid()}")
            .Generate();

        // Act
        var postPesponse = await client.PostAsJsonAsync("/api/v1/categories", data);

        // Assert
        Assert.False(postPesponse.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.Unauthorized, postPesponse.StatusCode);
    }
}