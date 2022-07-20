using KidsRUs.Application.Repositories;
using KidsRUs.Domain.Entities;
using KidsRUs.Persistence.Common;
using KidsRUs.Persistence.Context;

namespace KidsRUs.Persistence.Repositories;

public class TagRepository : BaseRepository<Tag>, ITagRepository
{
    public TagRepository(KidsRUsContext context) : base(context)
    {
    }
}