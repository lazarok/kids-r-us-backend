using KidsRUs.Application.Repositories;
using KidsRUs.Domain.Entities;
using KidsRUs.Persistence.Common;
using KidsRUs.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace KidsRUs.Persistence.Repositories;

public class UserRepository: BaseRepository<User>, IUserRepository
{
    private readonly KidsRUsContext _context;
    public UserRepository(KidsRUsContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<User> GetByIdAsync(int id)
    {
        return await _context.Users.Include(_ => _.Role).SingleOrDefaultAsync(_ => _.Id == id);
    }

    public async Task<User> FindByEmailAsync(string email)
    {
        return _context.Users.Include(_ => _.Role).SingleOrDefault(_ => _.Email == email);
    }
}