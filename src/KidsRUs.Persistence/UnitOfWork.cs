using KidsRUs.Application.Repositories;
using KidsRUs.Application.Repositories.Common;
using KidsRUs.Application.Services;
using KidsRUs.Persistence.Context;
using KidsRUs.Persistence.Repositories;
using Microsoft.Extensions.Configuration;

namespace KidsRUs.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly KidsRUsContext _context;

    public UnitOfWork(KidsRUsContext context)
    {
        _context = context;

        Category = new CategoryRepository(_context);
        Image = new ImageRepository(_context);
        Product = new ProductRepository(_context);
        Tag = new TagRepository(_context);
        User = new UserRepository(_context);
    }
    
    public ICategoryRepository Category { get; set; }
    public IImageRepository Image { get; set; }
    public IProductRepository Product { get; set; }
    public ITagRepository Tag { get; }
    public IUserRepository User { get; }

    public int Save()
    {
        return _context.SaveChanges();
    }
    
    public async Task<int> SaveAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}