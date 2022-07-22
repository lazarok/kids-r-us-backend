namespace KidsRUs.IntegrationTests.Features.Product;

public class GetSearchCountTests : TestBase
{
    [Fact]
    public async Task GetSearchCount()
    {
        // Arrenge
        var client = Application.CreateClient();

        // Act
        var response = await client.GetFromJsonAsync<ApiResponse<int>>("/api/v1/products/count");

        // Assert
        Assert.NotNull(response);
        Assert.IsType<ApiResponse<int>>(response);
        Assert.Equal(14, response.Data);
    }
}