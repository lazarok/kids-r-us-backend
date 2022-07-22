namespace KidsRUs.IntegrationTests.Features.Product;

public class GetProductsOutOfStockTests : TestBase
{
    [Fact]
    public async Task Products()
    {
        // Arrenge
        var client = Application.CreateClient();

        // Act
        var pagination = await client.GetFromJsonAsync<PaginationResponse<ProductVm>>("/api/v1/products/out-of-stock");

        // Assert
        Assert.NotNull(pagination);
        Assert.IsType<PaginationResponse<ProductVm>>(pagination);
        if (pagination != null)
        {
            Assert.Equal(10, pagination.PageSize);
            Assert.Equal(1, pagination.CurrentPage);
            Assert.Equal(1, pagination.TotalPages);
            Assert.Equal(2, pagination.TotalCount);
        }
    }
}