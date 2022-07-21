namespace KidsRUs.Application.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByIdAsync(int id);
    Task<User> FindByEmailAsync(string email);
}