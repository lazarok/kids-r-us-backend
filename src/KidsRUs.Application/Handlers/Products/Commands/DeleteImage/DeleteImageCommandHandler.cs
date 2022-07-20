using KidsRUs.Application.Services;

namespace KidsRUs.Application.Handlers.Products.Commands.DeleteImage;

public class DeleteImageCommandHandler: IRequestHandler<DeleteImageCommand, ApiResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductPictureService _productPictureService;

    public DeleteImageCommandHandler(IUnitOfWork unitOfWork, IProductPictureService productPictureService)
    {
        _unitOfWork = unitOfWork;
        _productPictureService = productPictureService;
    }

    public async Task<ApiResponse> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
    {
        var entity = _unitOfWork.Image.GetById(request.Sku.FromHashId());
        
        if (entity == null)
        {
            throw new NotFoundException("Image", request.Sku);
        }
        
        var fileName = entity.FileName;

        _unitOfWork.Image.Remove(entity);

        await _unitOfWork.SaveAsync(cancellationToken);

        if (await _productPictureService.HasPictureAsync(fileName, cancellationToken))
        {
            _productPictureService.DeletePictureAsync(fileName, cancellationToken);
        }

        return new ApiResponse();
    }
}