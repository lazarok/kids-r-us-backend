namespace KidsRUs.IntegrationTests.Features.Sale;

public class ProductsSoldTests : TestBase
{
    [Fact]
    public async Task ProductsSold()
    {
        // Arrenge
        var client = Application.CreateClient();

        // Act
        var pagination = await client.GetFromJsonAsync<PaginationResponse<ProductVm>>("/api/v1/sales/products-sold");

        // Assert
        
        // Assert
        Assert.NotNull(pagination);
        Assert.IsType<PaginationResponse<ProductVm>>(pagination);
        if (pagination != null)
        {
            Assert.Equal(10, pagination.PageSize);
            Assert.Equal(1, pagination.CurrentPage);
            Assert.Equal(1, pagination.TotalPages);
            Assert.Equal(5, pagination.TotalCount);
        }
    }
}