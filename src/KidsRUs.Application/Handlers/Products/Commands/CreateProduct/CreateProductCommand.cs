namespace KidsRUs.Application.Handlers.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<ApiResponse<ProductVm>>, IMapFrom<Product>
{
    public string CategorySku { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int ProductStock { get; set; }
    public decimal Price { get; set; }
    public decimal AverageRating { get; set; } = 5;
    public string? Info { get; set; }
    public List<string>? Tags { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateProductCommand, Product>()
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(c => c.CategorySku.FromHashId()))
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(c => default(List<Tag>)));
        
        profile.CreateMap<CreateProductDto, CreateProductCommand>();
    }
}