namespace KidsRUs.Application.Handlers.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRequest<ApiResponse<CategoryVm>>
{
    public string Sku { get; set; }
    public string Name { get; set; }
}