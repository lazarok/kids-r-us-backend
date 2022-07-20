using KidsRUs.Domain.Common;

namespace KidsRUs.Domain.Entities;

public class Product : BaseEntity
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int ProductStock { get; set; }
    public decimal Price { get; set; }
    public decimal AverageRating { get; set; }
    public string? Info { get; set; }  
    public bool Deleted { get; set; }
    public Category Category { get; set; }
    public IList<Tag> Tags { get; set; }
    public IList<Image> Images { get; set; }
    public IList<Sale> Sales { get; set; }
}