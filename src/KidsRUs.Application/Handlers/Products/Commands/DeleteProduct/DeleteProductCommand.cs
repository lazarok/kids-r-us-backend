namespace KidsRUs.Application.Handlers.Products.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest<ApiResponse>
{
    public string Sku { get; set; }
}