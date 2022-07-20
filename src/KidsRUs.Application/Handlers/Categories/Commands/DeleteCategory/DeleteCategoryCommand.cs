namespace KidsRUs.Application.Handlers.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommand : IRequest<ApiResponse>
{
    public string Sku { get; set; }
}