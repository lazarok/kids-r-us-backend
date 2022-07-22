using Bogus;

namespace KidsRUs.IntegrationTests.Seeds;

public static class SaleSeed
{
    public static async Task SeedDataAsync(IUnitOfWork unitOfWork)
    {
        var prices = new[] { 10, 5.25, 8, 50, 35 };
        
        for (var i = 1; i <= 5; i++)
        {
            unitOfWork.Sale.Add(new Faker<Sale>()
                .RuleFor(_ => _.ProductId, _ => i)
                .RuleFor(_ => _.Price, _ => (decimal)prices[i - 1])
                .RuleFor(_ => _.CreateAt, f => f.Date.Between(DateTime.UtcNow.Subtract(TimeSpan.FromDays(30)), DateTime.UtcNow))
                .Generate());
        }
        
        await unitOfWork.SaveAsync();
    }
}