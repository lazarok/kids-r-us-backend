namespace KidsRUs.IntegrationTests.Features.Sale;

public class GrossProfitTests : TestBase
{
    [Fact]
    public async Task GrossProfit()
    {
        // Arrenge
        var client = Application.CreateClient();

        // Act
        var response = await client.GetFromJsonAsync<ApiResponse<decimal>>("/api/v1/sales/gross-profit");

        // Assert
        
        // Assert
        Assert.NotNull(response);
        Assert.IsType<ApiResponse<decimal>>(response);
        Assert.Equal(108.25m, response.Data);
    }
}