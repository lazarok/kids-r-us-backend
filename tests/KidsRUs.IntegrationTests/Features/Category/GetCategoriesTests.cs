using KidsRUs.Application.Models.ViewModels;

namespace KidsRUs.IntegrationTests.Features.Category;

public class GetCategoriesTests : TestBase
{
    [Fact]
    public async Task Categories()
    {
        // Arrenge
        var client = Application.CreateClient();

        // Act
        var pagination = await client.GetFromJsonAsync<PaginationResponse<CategoryVm>>("/api/v1/categories");

        // Assert
        Assert.NotNull(pagination);
        Assert.IsType<PaginationResponse<CategoryVm>>(pagination);
        Assert.Equal(pagination.PageSize, 10);
        Assert.Equal(pagination.CurrentPage, 1);
        Assert.Equal(pagination.TotalPages, 2);
    }
    
    [Fact]
    public async Task Categories_WithPagination()
    {
        // Arrenge
        var client = Application.CreateClient();

        // Act
        var pagination = await client.GetFromJsonAsync<PaginationResponse<CategoryVm>>("/api/v1/categories?PageNumber=2&PageSize=12");

        // Assert
        Assert.NotNull(pagination);
        Assert.IsType<PaginationResponse<CategoryVm>>(pagination);
        Assert.Equal(pagination.PageSize, 12);
        Assert.Equal(pagination.CurrentPage, 2);
        Assert.Equal(pagination.TotalPages, 2);
        Assert.Equal(pagination.Data.Count, 6);
    }
}