namespace KidsRUs.Application.Handlers.Sales.Commands.CreateSale;

public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, ApiResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateSaleCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Product.GetById(request.ProductSku.FromHashId());

        if (entity == null)
        {
            throw new NotFoundException("Product", request.ProductSku);
        }

        if (entity.ProductStock <= 0)
        {
            throw new CustomException($"Product '{request.ProductSku}' has no stock", errorStatus: ErrorStatus.Validation);
        }

        entity.ProductStock--;
        
        _unitOfWork.Sale.Add(new Sale
        {
            ProductId = entity.Id,
            Price = entity.Price,
            CreateAt = DateTime.UtcNow
        });
        
        await _unitOfWork.SaveAsync(cancellationToken);
        
        return new ApiResponse();
    }
}