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
        if (pagination != null)
        {
            Assert.Equal(10, pagination.PageSize);
            Assert.Equal(1, pagination.CurrentPage);
            Assert.Equal(2, pagination.TotalPages);
        }
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
        if (pagination != null)
        {
            Assert.Equal(12, pagination.PageSize);
            Assert.Equal(2, pagination.CurrentPage);
            Assert.Equal(2, pagination.TotalPages);
            Assert.Equal(6, pagination.Data.Count);
        }
    }
}