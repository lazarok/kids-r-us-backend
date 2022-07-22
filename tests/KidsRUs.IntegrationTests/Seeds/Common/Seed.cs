namespace KidsRUs.IntegrationTests.Seeds.Common;

public static class Seed
{
    public static async Task SeedDataAsync(IUnitOfWork unitOfWork)
    { 
        await CategorySeed.SeedDataAsync(unitOfWork);
        await ProductSeed.SeedDataAsync(unitOfWork);
        await TagSeed.SeedDataAsync(unitOfWork);
        await SaleSeed.SeedDataAsync(unitOfWork);
    }
}