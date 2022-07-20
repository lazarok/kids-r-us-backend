using KidsRUs.Application.QueryFilters;
using KidsRUs.Application.Repositories;
using KidsRUs.Domain.Entities;
using KidsRUs.Persistence.Common;
using KidsRUs.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace KidsRUs.Persistence.Repositories;

public class SaleRepository: BaseRepository<Sale>, ISaleRepository
{
    private readonly KidsRUsContext _context;

    public SaleRepository(KidsRUsContext context) : base(context)
    {
        _context = context;
    }
    
    public IQueryable<Product> GetProductsSold(ProductSoldFilter filter)
    {
        var query = from s in _context.Sales
            join p in _context.Products.Include(_ => _.Category).Include(_ => _.Tags)
                on s.ProductId equals p.Id
            select p;

        var products = query.Distinct();

        if (!string.IsNullOrEmpty(filter.Search))
        {
            products = products.Where(_ => _.Name.ToLower().Contains(filter.Search.ToLower()));
            products = products.Where(_ => _.Description.ToLower().Contains(filter.Search.ToLower()));
        }
        
        return products;
    }

    public async Task<decimal> GetGrossProfit(GrossProfitFilter filter)
    {
        var query = _context.Sales.AsQueryable();
        
        if (filter.MinDate != null && filter.MaxDate != null && filter.MinDate <= filter.MaxDate)
        {
            query = query.Where(_ => _.CreateAt.Date >= filter.MinDate.Value.Date && _.CreateAt <= filter.MaxDate.Value.Date);
        }

        return (decimal) await query.SumAsync(_ => (double)_.Price);
    }
}