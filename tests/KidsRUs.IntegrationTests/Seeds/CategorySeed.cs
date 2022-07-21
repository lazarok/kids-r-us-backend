namespace KidsRUs.IntegrationTests.Seeds;

public static class CategorySeed
{
    public static async Task SeedDataAsync(IUnitOfWork unitOfWork)
    {
        for (var i = 1; i <= 18; i++)
        {
            unitOfWork.Category.Add(new Category
            {
                Name = $"Category{i}"
            });
        }

        await unitOfWork.SaveAsync();
    }
}