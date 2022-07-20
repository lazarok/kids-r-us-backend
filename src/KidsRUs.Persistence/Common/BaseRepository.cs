using System.Linq.Expressions;
using KidsRUs.Application.Repositories.Common;
using KidsRUs.Persistence.Context;

namespace KidsRUs.Persistence.Common;

public class BaseRepository<T> : IRepository<T> where T : class
{
    private readonly KidsRUsContext _context;

    public BaseRepository(KidsRUsContext context)
    {
        _context = context;
    }

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void AddRange(IEnumerable<T>? entities)
    {
        _context.Set<T>().AddRange(entities);
    }

    public IQueryable<T> Find(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression);
    }

    public IQueryable<T> GetAll()
    {
        return _context.Set<T>().AsQueryable();
    }

    public T GetById(int id)
    {
        return _context.Set<T>().Find(id);
    }
    
    public bool Any(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Any(expression);
    }

    public void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }
}