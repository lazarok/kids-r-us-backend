using System.Linq.Expressions;

namespace KidsRUs.Application.Repositories.Common;

public interface IRepository<T> where T : class
{
    T GetById(int id);
    IQueryable<T> GetAll();
    IQueryable<T> Find(Expression<Func<T, bool>> expression);
    void Add(T entity);
    bool Any(Expression<Func<T, bool>> expression);
    void AddRange(IEnumerable<T>? entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}