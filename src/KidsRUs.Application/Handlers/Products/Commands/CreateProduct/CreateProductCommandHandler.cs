namespace KidsRUs.Application.Handlers.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ApiResponse<ProductVm>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse<ProductVm>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Product>(request);

        _unitOfWork.Product.Add(entity);
        await _unitOfWork.SaveAsync(cancellationToken);

        _unitOfWork.Tag.AddRange(request.Tags?.Select(tagName => new Tag {ProductId = entity.Id, Name = tagName}));
        await _unitOfWork.SaveAsync(cancellationToken);

        entity = await _unitOfWork.Product.GetById(entity.Id);

        var response = new ApiResponse<ProductVm>
        {
            Data = _mapper.Map<ProductVm>(entity)
        };

        return response;
    }
}