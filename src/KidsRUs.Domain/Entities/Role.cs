using KidsRUs.Domain.Common;

namespace KidsRUs.Domain.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; }
    public IList<User> Users { get; set; }
}