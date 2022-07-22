namespace KidsRUs.IntegrationTests.Features.Product;

public class GetProductsTests : TestBase
{
    [Fact]
    public async Task Products()
    {
        // Arrenge
        var client = Application.CreateClient();

        // Act
        var pagination = await client.GetFromJsonAsync<PaginationResponse<ProductVm>>("/api/v1/products");

        // Assert
        Assert.NotNull(pagination);
        Assert.IsType<PaginationResponse<ProductVm>>(pagination);
        if (pagination != null)
        {
            Assert.Equal(10, pagination.PageSize);
            Assert.Equal(1, pagination.CurrentPage);
            Assert.Equal(2, pagination.TotalPages);
            Assert.Equal(14, pagination.TotalCount);
        }
    }
    
    [Fact]
    public async Task Products_WithPagination()
    {
        // Arrenge
        var client = Application.CreateClient();

        // Act
        var pagination = await client.GetFromJsonAsync<PaginationResponse<ProductVm>>("/api/v1/products?PageNumber=2&PageSize=12");

        // Assert
        Assert.NotNull(pagination);
        Assert.IsType<PaginationResponse<ProductVm>>(pagination);
        if (pagination != null)
        {
            Assert.Equal(12, pagination.PageSize);
            Assert.Equal(2, pagination.CurrentPage);
            Assert.Equal(2, pagination.TotalPages);
            Assert.Equal(2, pagination.Data.Count);
        }
    }
}