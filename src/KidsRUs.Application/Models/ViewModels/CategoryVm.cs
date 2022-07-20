namespace KidsRUs.Application.Models.ViewModels;

public class CategoryVm : IMapFrom<Category>
{
    public string Sku { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Category, CategoryVm>()
            .ForMember(dest => dest.Sku, opt => opt.MapFrom(c => c.Id.ToHashId()));
    }
}