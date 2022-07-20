using KidsRUs.Domain.Common;

namespace KidsRUs.Domain.Entities;

public class Sale : BaseEntity
{
    public int ProductId { get; set; }
    public decimal Price { get; set; }
    public DateTime CreateAt { get; set; }
    public Product Product { get; set; }
}