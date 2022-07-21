using KidsRUs.Application.Repositories.Common;

namespace KidsRUs.IntegrationTests.Seeds;

public static class BaseSeed
{
    public static async Task SeedDataAsync(IUnitOfWork unitOfWork)
    {
        await CategorySeed.SeedDataAsync(unitOfWork);
    }
}