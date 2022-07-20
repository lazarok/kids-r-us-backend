using KidsRUs.Application.Models.Dtos;

namespace KidsRUs.Application.Handlers.Products.Commands.AddImage;

public class AddImageCommand : IRequest<ApiResponse<ImageVm>>
{
    public string ProductSku { get; set; }
    public MediaFileDto MediaFile { get; set; } 
}