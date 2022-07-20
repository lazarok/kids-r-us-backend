namespace KidsRUs.Application.Handlers.Tags.Commands.DeleteTag;

public class DeleteTagCommand : IRequest<ApiResponse>
{
    public string Sku { get; set; }
}