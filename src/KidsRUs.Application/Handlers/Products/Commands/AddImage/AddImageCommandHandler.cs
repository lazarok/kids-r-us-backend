using KidsRUs.Application.Services;
using Microsoft.Extensions.Configuration;

namespace KidsRUs.Application.Handlers.Products.Commands.AddImage;

public class AddImageCommandHandler : IRequestHandler<AddImageCommand, ApiResponse<ImageVm>>
{
    private readonly IProductPictureService _productPictureService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUriService _uriService;
    private readonly string _containerName;

    public AddImageCommandHandler(IProductPictureService productPictureService, IMapper mapper, IUnitOfWork unitOfWork,
        IUriService uriService, IConfiguration configuration)
    {
        _productPictureService = productPictureService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _uriService = uriService;
        _containerName = configuration["ProductServiceOptions:ContainerName"];
    }

    public async Task<ApiResponse<ImageVm>> Handle(AddImageCommand request, CancellationToken cancellationToken)
    {
        if (request.MediaFile.FileContent.Length == 0)
        {
            throw new CustomException("File is required", errorStatus: ErrorStatus.Validation);
        }

        var productId = request.ProductSku.FromHashId();

        if (!_unitOfWork.Product.Any(_ => _.Id == productId))
        {
            throw new NotFoundException("Product", request.ProductSku);
        }

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.MediaFile.FileName)}";

        await _productPictureService.SavePictureAsync(fileName, request.MediaFile.FileContent, cancellationToken);

        var image = new Image
        {
            ProductId = productId,
            FileName = fileName
        };

        _unitOfWork.Image.Add(image);

        await _unitOfWork.SaveAsync(cancellationToken);

        var response = new ApiResponse<ImageVm>
        {
            Data = await _unitOfWork.Image.GetImageAsync(_uriService, _containerName, image.Id)
        };

        return response;
    }
}