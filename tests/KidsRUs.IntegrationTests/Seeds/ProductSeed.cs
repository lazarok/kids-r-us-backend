using Bogus;
using KidsRUs.Application.Helper;
using Microsoft.EntityFrameworkCore;

namespace KidsRUs.IntegrationTests.Seeds;

public static class ProductSeed
{
    public static async Task SeedDataAsync(IUnitOfWork unitOfWork)
    {
        // categoryId1 and categoryId2 are used for category tests.
        // A category associated with a product cannot be deleted.
        
        var categoryId1 = "Agrlprx5".FromHashId();
        var categoryId2 = "JwRPo2e7".FromHashId();

        var categoriesId = await unitOfWork.Category.Find(_ => _.Id != categoryId1 && _.Id != categoryId2)
            .Select(_ => _.Id).ToListAsync();

        for (var i = 1; i <= 10; i++)
        {
            var product = new Faker<Product>()
                .RuleFor(_ => _.CategoryId, f => f.PickRandom(categoriesId))
                .RuleFor(_ => _.Name, f => $"{f.Commerce.ProductName()}-{Guid.NewGuid()}")
                .RuleFor(_ => _.Description, f => f.Commerce.ProductDescription())
                .RuleFor(_ => _.ProductStock, f => f.Random.Int(1, 200))
                .RuleFor(_ => _.Price, f => decimal.Parse(f.Commerce.Price(1, 100)))
                .RuleFor(_ => _.AverageRating, f => decimal.Parse(f.Commerce.Price(1, 5, 1)))
                .RuleFor(_ => _.Info, f => f.Lorem.Text())
                .Generate();
            
            unitOfWork.Product.Add(product);
        }
        
        unitOfWork.Product.Add(new Faker<Product>()
            .RuleFor(_ => _.CategoryId, f => f.PickRandom(categoriesId))
            .RuleFor(_ => _.Name, f => $"ProductName-{Guid.NewGuid()}")
            .RuleFor(_ => _.Description, f => f.Random.ClampString(f.Commerce.ProductDescription(),1, 100))
            .RuleFor(_ => _.ProductStock, f => f.Random.Int(1, 200))
            .RuleFor(_ => _.Price, f => decimal.Parse(f.Commerce.Price(1, 100)))
            .RuleFor(_ => _.AverageRating, f => decimal.Parse(f.Commerce.Price(1, 5, 1)))
            .RuleFor(_ => _.Info, f => f.Lorem.Text())
            .Generate());


        unitOfWork.Product.Add(new Faker<Product>()
            .RuleFor(_ => _.CategoryId, f => f.PickRandom(categoriesId))
            .RuleFor(_ => _.Name, f => $"{f.Commerce.ProductName()}-{Guid.NewGuid()}")
            .RuleFor(_ => _.Description, f => f.Commerce.ProductDescription())
            .RuleFor(_ => _.ProductStock, f => f.Random.Int(1, 200))
            .RuleFor(_ => _.Price, f => decimal.Parse(f.Commerce.Price(1, 100)))
            .RuleFor(_ => _.AverageRating, f => decimal.Parse(f.Commerce.Price(1, 5, 1)))
            .Generate());

        unitOfWork.Product.Add(new Faker<Product>()
            .RuleFor(_ => _.CategoryId, f => f.PickRandom(categoriesId))
            .RuleFor(_ => _.Name, f => $"ProductName-{Guid.NewGuid()}")
            .RuleFor(_ => _.Description, f => f.Commerce.ProductDescription())
            .RuleFor(_ => _.ProductStock, f => f.Random.Int(1, 200))
            .RuleFor(_ => _.Price, f => decimal.Parse(f.Commerce.Price(1, 100)))
            .RuleFor(_ => _.AverageRating, f => decimal.Parse(f.Commerce.Price(1, 5, 1)))
            .RuleFor(_ => _.Info, f => f.Lorem.Text())
            .RuleFor(_ => _.Deleted, _ => true)
            .Generate());
        
        unitOfWork.Product.Add(new Faker<Product>()
            .RuleFor(_ => _.CategoryId, f => f.PickRandom(categoriesId))
            .RuleFor(_ => _.Name, f => $"{f.Commerce.ProductName()}-{Guid.NewGuid()}")
            .RuleFor(_ => _.Description, f => f.Commerce.ProductDescription())
            .RuleFor(_ => _.ProductStock, _ => 0)
            .RuleFor(_ => _.Price, f => decimal.Parse(f.Commerce.Price(1, 100)))
            .RuleFor(_ => _.AverageRating, f => decimal.Parse(f.Commerce.Price(1, 5, 1)))
            .Generate());
        
        unitOfWork.Product.Add(new Faker<Product>()
            .RuleFor(_ => _.CategoryId, f => f.PickRandom(categoriesId))
            .RuleFor(_ => _.Name, f => $"ProductName-{Guid.NewGuid()}")
            .RuleFor(_ => _.Description, f => f.Random.ClampString(f.Commerce.ProductDescription(),1, 100))
            .RuleFor(_ => _.ProductStock, _ => 0)
            .RuleFor(_ => _.Price, f => decimal.Parse(f.Commerce.Price(1, 100)))
            .RuleFor(_ => _.AverageRating, f => decimal.Parse(f.Commerce.Price(1, 5, 1)))
            .RuleFor(_ => _.Info, f => f.Lorem.Text())
            .Generate());

        await unitOfWork.SaveAsync();
    }
}