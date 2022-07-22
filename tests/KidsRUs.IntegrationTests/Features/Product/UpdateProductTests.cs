using System.Net;
using Bogus;
using KidsRUs.Application.Handlers.Products.Commands.UpdateProduct;

namespace KidsRUs.IntegrationTests.Features.Product;

public class UpdateProductTests : TestBase
{
    [Theory]
    [InlineData("Agrlprx5")]
    [InlineData("JwRPo2e7")]
    public async Task Product_IsUpdated_WhenValidFieldsAreProvided_WithAdminUser(string productSku)
    {
        // Arrenge
        var (client, _) = await GetClientAsAdmin();
        var data = new Faker<UpdateProductDto>()
            .RuleFor(_ => _.Name, _ => "Product1")
            .RuleFor(_ => _.Description, f => f.Random.ClampString(f.Commerce.ProductDescription(),1, 100))
            .RuleFor(_ => _.ProductStock, f => f.Random.Int(1, 200))
            .RuleFor(_ => _.Price, f => decimal.Parse(f.Commerce.Price(1, 100)))
            .RuleFor(_ => _.AverageRating, f => decimal.Parse(f.Commerce.Price(1, 5, 1)))
            .RuleFor(_ => _.Info, f => f.Lorem.Text())
            .Generate();

        // Act
        var response = await client.PutAsJsonAsync($"/api/v1/products/{productSku}", data);

        // Assert
        
        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<ProductVm>>();
        Assert.NotNull(apiResponse);
        Assert.NotNull(apiResponse.Data);
        Assert.Equal(data.Name, apiResponse.Data.Name);
        
        var newProduct = await client.GetFromJsonAsync<ApiResponse<ProductVm>>($"/api/v1/products/{apiResponse.Data.Sku}");
        Assert.NotNull(newProduct);
        Assert.NotNull(newProduct.Data);
        Assert.Equal(newProduct.Data.Sku, apiResponse.Data.Sku);
    }
    
    [Theory]
    [InlineData("43gsf43gs")]
    [InlineData("wwedsfd232")]
    public async Task Product_IsNotUpdated_WhenProductDontExist_WithAdminUser(string productSku)
    {
        // Arrenge
        var (client, _) = await GetClientAsAdmin();
        var data = new Faker<UpdateProductDto>()
            .RuleFor(_ => _.Name, _ => "Product1")
            .RuleFor(_ => _.Description, f => f.Random.ClampString(f.Commerce.ProductDescription(),1, 100))
            .RuleFor(_ => _.ProductStock, f => f.Random.Int(1, 200))
            .RuleFor(_ => _.Price, f => decimal.Parse(f.Commerce.Price(1, 100)))
            .RuleFor(_ => _.AverageRating, f => decimal.Parse(f.Commerce.Price(1, 5, 1)))
            .RuleFor(_ => _.Info, f => f.Lorem.Text())
            .Generate();

        // Act
        var response = await client.PutAsJsonAsync($"/api/v1/products/{productSku}", data);

        // Assert
        
        Assert.False(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        
        var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        Assert.NotNull(errorResponse);
    }
}