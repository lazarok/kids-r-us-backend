namespace KidsRUs.Application.Repositories;

public interface ISaleRepository : IRepository<Sale>
{
    IQueryable<Product> GetProductsSold(ProductSoldFilter filter);
    Task<decimal> GetGrossProfit(GrossProfitFilter filter);
}