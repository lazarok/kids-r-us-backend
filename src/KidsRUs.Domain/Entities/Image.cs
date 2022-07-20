using KidsRUs.Domain.Common;

namespace KidsRUs.Domain.Entities;

public class Image : BaseEntity
{
    public int ProductId { get; set; }
    public string FileName { get; set; }
    public Product Product { get; set; }
}