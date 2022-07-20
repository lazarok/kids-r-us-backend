namespace KidsRUs.Application.Models.ViewModels;

public class TagVm : IMapFrom<Tag>
{
    public string Sku { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Tag, TagVm>()
            .ForMember(dest => dest.Sku, opt => opt.MapFrom(t => t.Id.ToHashId()));
    }
}