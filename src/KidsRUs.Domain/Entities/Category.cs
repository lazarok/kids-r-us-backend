using KidsRUs.Domain.Common;

namespace KidsRUs.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; }
    public IList<Product> Products { get; set; }
}