using KidsRUs.Application.Repositories;
using KidsRUs.Domain.Entities;
using KidsRUs.Persistence.Common;
using KidsRUs.Persistence.Context;

namespace KidsRUs.Persistence.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(KidsRUsContext context) : base(context)
    {
    }
}