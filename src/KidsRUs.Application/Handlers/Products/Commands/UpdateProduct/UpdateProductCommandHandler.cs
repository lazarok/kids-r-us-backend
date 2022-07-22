namespace KidsRUs.Application.Handlers.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ApiResponse<ProductVm>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse<ProductVm>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Product.GetById(request.Sku.FromHashId());
        
        if (entity == null)
        {
            throw new NotFoundException("Product", request.Sku);
        }
        
        if (_unitOfWork.Product.Any(_ => _.Id != entity.Id && _.Name == request.Name))
        {
            throw new CustomException("Product name is in used", errorStatus: ErrorStatus.Validation);
        }

        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.Price = request.Price;
        entity.AverageRating = request.AverageRating;
        entity.Info = request.Info;

        await _unitOfWork.SaveAsync(cancellationToken);

        var response = new ApiResponse<ProductVm>
        {
            Data = _mapper.Map<ProductVm>(entity)
        };

        return response;
    }
}