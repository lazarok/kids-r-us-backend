namespace KidsRUs.IntegrationTests.Seeds;

public static class TagSeed
{
    public static async Task SeedDataAsync(IUnitOfWork unitOfWork)
    {
        for (var productId = 1; productId <=2; productId++)
        {
            for (var i = 1; i <=2; i++)
            {
                unitOfWork.Tag.Add(new Tag
                {
                    ProductId = productId,
                    Name = $"{productId}_{Guid.NewGuid()}"
                });
            }
        }
        
        await unitOfWork.SaveAsync();
    }
}