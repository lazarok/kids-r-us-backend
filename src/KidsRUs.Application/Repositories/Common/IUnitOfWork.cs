namespace KidsRUs.Application.Repositories.Common;

public interface IUnitOfWork : IDisposable
{
    ICategoryRepository Category { get; set; }
    IImageRepository Image { get; set; }
    IProductRepository Product { get; set; }
    ISaleRepository Sale { get; set; }
    ITagRepository Tag { get; }
    IUserRepository User { get; }
    Task<int> SaveAsync(CancellationToken cancellationToken = default);
    int Save();
}