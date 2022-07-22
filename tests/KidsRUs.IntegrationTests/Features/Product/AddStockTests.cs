using System.Net;
using KidsRUs.Application.Handlers.Products.Commands.AddStock;

namespace KidsRUs.IntegrationTests.Features.Product;

public class AddStockTests : TestBase
{
    [Theory]
    [InlineData("oWRgj2Bp", 23)] // productId: 14
    [InlineData("KprwL1W9", 100)] // productId: 15
    public async Task Product_IsStoked_WhenValidFieldsAreProvided_WithAdminUser(string productSku, int stock)
    {
        // Arrenge
        var (client, _) = await GetClientAsAdmin();
        var data = new AddStockDto {ProductStock = stock};

        // Act
        var response = await client.PutAsJsonAsync($"/api/v1/products/stock/{productSku}", data);

        // Assert
        
        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<ProductVm>>();
        Assert.NotNull(apiResponse);
        Assert.NotNull(apiResponse.Data);
        Assert.Equal(data.ProductStock, apiResponse.Data.ProductStock);
    }
    
    [Theory]
    [InlineData("oWRgj2Bp", 0)] // productId: 14
    [InlineData("KprwL1W9", -1)] // productId: 15
    public async Task Product_IsNotStoked_WhenStockIsLessThanOne_WithAdminUser(string productSku, int stock)
    {
        // Arrenge
        var (client, _) = await GetClientAsAdmin();
        var data = new AddStockDto {ProductStock = stock};

        // Act
        var response = await client.PutAsJsonAsync($"/api/v1/products/stock/{productSku}", data);

        // Assert
        
        Assert.False(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        
        var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        Assert.NotNull(errorResponse);
    }
}