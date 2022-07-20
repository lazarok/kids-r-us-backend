namespace KidsRUs.Application.Handlers.Products.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest<ApiResponse<ProductVm>>, IMapFrom<Product>
{
    public string Sku { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int ProductStock { get; set; }
    public decimal Price { get; set; }
    public decimal AverageRating { get; set; }
    public string? Info { get; set; }
}