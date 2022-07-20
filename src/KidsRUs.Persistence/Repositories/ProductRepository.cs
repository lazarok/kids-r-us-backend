using KidsRUs.Application.QueryFilters;
using KidsRUs.Application.Repositories;
using KidsRUs.Domain.Entities;
using KidsRUs.Persistence.Common;
using KidsRUs.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace KidsRUs.Persistence.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    private readonly KidsRUsContext _context;

    public ProductRepository(KidsRUsContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Product> GetById(int id)
    {
        return await _context.Products
            .Include(_ => _.Category)
            .Include(_ => _.Tags)
            .SingleOrDefaultAsync(_ => _.Id == id && !_.Deleted);
    }

    public IQueryable<Product> GetAll(ProductFilter filter)
    {
        var queryable = _context.Products
            .Include(_ => _.Category)
            .Include(_ => _.Tags)
            .Where(_ => !_.Deleted)
            .AsQueryable();
        
        if (!string.IsNullOrEmpty(filter.Name))
        {
            queryable = queryable.Where(_ => _.Name.ToLower().Contains(filter.Name.ToLower()));
        }
        
        if (!string.IsNullOrEmpty(filter.Description))
        {
            queryable = queryable.Where(_ => _.Description.ToLower().Contains(filter.Description.ToLower()));
        }
        
        if (!string.IsNullOrEmpty(filter.Info))
        {
            queryable = queryable.Where(_ => _.Info.ToLower().Contains(filter.Info.ToLower()));
        }
        
        if (!string.IsNullOrEmpty(filter.CategoryName))
        {
            queryable = queryable.Where(_ => _.Category.Name.ToLower().Contains(filter.CategoryName.ToLower()));
        }
        
        if (!string.IsNullOrEmpty(filter.TagName))
        {
            queryable = queryable.Where(_ => 
                _.Tags.Any(
                    t => t.Name.ToLower().Contains(filter.CategoryName.ToLower())));
        }
        
        if (filter.MinPrice != null && filter.MaxPrice != null)
        {
            queryable = queryable.Where(_ => _.Price >= filter.MinPrice && _.Price <= filter.MaxPrice);
        }

        return queryable;
    }

    public IQueryable<Product> GetProductsOutOfStock(ProductsOutOfStockFilter filter)
    {
        var queryable = _context.Products
            .Include(_ => _.Category)
            .Include(_ => _.Tags)
            .Where(_ => _.ProductStock <= 0 && !_.Deleted)
            .AsQueryable();
        
        if (!string.IsNullOrEmpty(filter.Search))
        {
            queryable = queryable.Where(_ => _.Name.ToLower().Contains(filter.Search.ToLower()));
            queryable = queryable.Where(_ => _.Description.ToLower().Contains(filter.Search.ToLower()));
        }
        
        return queryable;
    }
}