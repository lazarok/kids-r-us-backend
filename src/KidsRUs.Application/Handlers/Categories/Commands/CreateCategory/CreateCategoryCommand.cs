namespace KidsRUs.Application.Handlers.Categories.Commands.CreateCategory;

public class CreateCategoryCommand : IRequest<ApiResponse<CategoryVm>>, IMapFrom<Category>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateCategoryCommand, Category>();
        profile.CreateMap<CreateCategoryDto, CreateCategoryCommand>();
    }
}