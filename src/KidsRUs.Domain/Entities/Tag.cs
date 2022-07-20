using KidsRUs.Domain.Common;

namespace KidsRUs.Domain.Entities;

public class Tag : BaseEntity
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public Product Product { get; set; }
}