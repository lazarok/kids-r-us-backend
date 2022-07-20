namespace KidsRUs.Application.Handlers.Products.Commands.CreateProduct;

public class CreateProductDto
{
    public string CategorySku { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int ProductStock { get; set; }
    public decimal Price { get; set; }
    public decimal AverageRating { get; set; }
    public string? Info { get; set; }
    public List<string>? Tags { get; set; }
}