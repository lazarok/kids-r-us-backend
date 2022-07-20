using KidsRUs.Application.Services;

namespace KidsRUs.Application.Models.ViewModels;

public class ImageVm: IMapFrom<Image>
{
    public string Sku { get; set; }
    public string Url { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Image, ImageVm>()
            .ForMember(dest => dest.Sku, opt => opt.MapFrom(p => p.Id.ToHashId()));
    }
}