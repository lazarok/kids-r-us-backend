using KidsRUs.Application.Repositories.Common;

namespace KidsRUs.Application.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User> FindByEmailAsync(string email);
}