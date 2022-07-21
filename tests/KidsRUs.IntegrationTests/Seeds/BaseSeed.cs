namespace KidsRUs.IntegrationTests.Seeds;

public static class BaseSeed
{
    public static Task SeedDataAsync(IUnitOfWork unitOfWork)
    {
        return CategorySeed.SeedDataAsync(unitOfWork);
    }
}