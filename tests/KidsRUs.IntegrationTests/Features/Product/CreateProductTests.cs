using System.Net;
using Bogus;
using KidsRUs.Application.Handlers.Products.Commands.CreateProduct;

namespace KidsRUs.IntegrationTests.Features.Product;

public class CreateProductTests : TestBase
{
    [Theory]
    [InlineData("Product1")]
    [InlineData("Product2")]
    public async Task Product_IsCreated_WhenValidFieldsAreProvided_WithAdminUser(string productName)
    {
        // Arrenge
        var (client, _) = await GetClientAsAdmin();
        var data = new Faker<CreateProductDto>()
            .RuleFor(_ => _.CategorySku, _ => "Agrlprx5")
            .RuleFor(_ => _.Name, _ => productName)
            .RuleFor(_ => _.Description, f => f.Random.ClampString(f.Commerce.ProductDescription(),1, 100))
            .RuleFor(_ => _.ProductStock, f => f.Random.Int(1, 200))
            .RuleFor(_ => _.Price, f => decimal.Parse(f.Commerce.Price(1, 100)))
            .RuleFor(_ => _.AverageRating, f => decimal.Parse(f.Commerce.Price(1, 5, 1)))
            .RuleFor(_ => _.Info, f => f.Lorem.Text())
            .RuleFor(_ => _.Tags, f => f.Lorem.Words(f.Random.Int(1, 5)).ToList())
            .Generate();

        // Act
        var response = await client.PostAsJsonAsync("/api/v1/products", data);

        // Assert
        
        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        
        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<ProductVm>>();
        Assert.NotNull(apiResponse);
        Assert.IsType<ApiResponse<ProductVm>>(apiResponse);
        
        Assert.Equal(productName, apiResponse.Data.Name);
    }
    
    
    [Theory]
    [InlineData("Product1")]
    [InlineData("Product2")]
    public async Task Product_IsNotCreated_WhenValidFieldsAreProvided_WithAnonymUser(string productName)
    {
        // Arrenge
        var client = Application.CreateClient();
        var data = new Faker<CreateProductDto>()
            .RuleFor(_ => _.CategorySku, _ => "Agrlprx5")
            .RuleFor(_ => _.Name, _ => productName)
            .RuleFor(_ => _.Description, f => f.Random.ClampString(f.Commerce.ProductDescription(),1, 100))
            .RuleFor(_ => _.ProductStock, f => f.Random.Int(1, 200))
            .RuleFor(_ => _.Price, f => decimal.Parse(f.Commerce.Price(1, 100)))
            .RuleFor(_ => _.AverageRating, f => decimal.Parse(f.Commerce.Price(1, 5, 1)))
            .RuleFor(_ => _.Info, f => f.Lorem.Text())
            .RuleFor(_ => _.Tags, f => f.Lorem.Words(f.Random.Int(1, 5)).ToList())
            .Generate();

        // Act
        var postPesponse = await client.PostAsJsonAsync("/api/v1/products", data);

        // Assert
        Assert.False(postPesponse.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.Unauthorized, postPesponse.StatusCode);
        
        var errorResponse = await postPesponse.Content.ReadFromJsonAsync<ErrorResponse>();
        Assert.NotNull(errorResponse);
    }
}