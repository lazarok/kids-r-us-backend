namespace KidsRUs.Application.Repositories;

public interface IProductRepository: IRepository<Product>
{
    Task<Product> GetById(int id);
    IQueryable<Product> GetAll(ProductFilter filter);
}