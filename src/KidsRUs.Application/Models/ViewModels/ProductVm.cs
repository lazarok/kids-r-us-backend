namespace KidsRUs.Application.Models.ViewModels;

public class ProductVm : IMapFrom<Product>
{ 
    public string Sku { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int ProductStock { get; set; }
    public decimal Price { get; set; }
    public decimal AverageRating { get; set; }
    public string? Info { get; set; }  
    public CategoryVm Category { get; set; }
    public List<TagVm> Tags { get; set; }
    public List<ImageVm> Images { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ProductVm>()
            .ForMember(dest => dest.Sku, opt => opt.MapFrom(p => p.Id.ToHashId()))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(p => p.Category))
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(p => p.Tags))
            .AfterMap((src, dest) => dest.Images = null);
    }
}