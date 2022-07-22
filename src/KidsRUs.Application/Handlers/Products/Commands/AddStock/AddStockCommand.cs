namespace KidsRUs.Application.Handlers.Products.Commands.AddStock;

public class AddStockCommand : IRequest<ApiResponse<ProductVm>>
{
    public string ProductSku { get; set; }
    public int ProductStock { get; set; }
}