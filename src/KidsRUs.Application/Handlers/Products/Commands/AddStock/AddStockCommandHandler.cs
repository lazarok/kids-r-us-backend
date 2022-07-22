namespace KidsRUs.Application.Handlers.Products.Commands.AddStock;

public class AddStockCommandHandler: IRequestHandler<AddStockCommand, ApiResponse<ProductVm>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddStockCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse<ProductVm>> Handle(AddStockCommand request, CancellationToken cancellationToken)
    {
        if (request.ProductStock < 1)
        {
            throw new CustomException("Product stock greater than 0 is required", errorStatus: ErrorStatus.Validation);
        }
        
        var entity = await _unitOfWork.Product.GetById(request.ProductSku.FromHashId());
        
        if (entity == null)
        {
            throw new NotFoundException("Product", request.ProductSku);
        }

        entity.ProductStock += request.ProductStock;
        
        await _unitOfWork.SaveAsync(cancellationToken);

        var response = new ApiResponse<ProductVm>
        {
            Data = _mapper.Map<ProductVm>(entity)
        };

        return response;
    }
}