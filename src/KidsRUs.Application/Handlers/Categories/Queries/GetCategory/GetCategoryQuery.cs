namespace KidsRUs.Application.Handlers.Categories.Queries.GetCategory;

public record GetCategoryQuery(string Sku) : IRequest<ApiResponse<CategoryVm>> {}