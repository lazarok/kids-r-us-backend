namespace KidsRUs.Application.Handlers.Tags.Commands.CreateTag;

public class CreateTagCommand : IRequest<ApiResponse<TagVm>>, IMapFrom<Tag>
{
    public string ProductSku { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateTagCommand, Tag>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(c => c.ProductSku.FromHashId()));
    }
}