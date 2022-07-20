namespace KidsRUs.Application.Handlers.Products.Commands.DeleteImage;

public class DeleteImageCommand : IRequest<ApiResponse>
{
    public string Sku { get; set; }
}