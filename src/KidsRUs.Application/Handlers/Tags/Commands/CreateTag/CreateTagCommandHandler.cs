namespace KidsRUs.Application.Handlers.Tags.Commands.CreateTag;

public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, ApiResponse<TagVm>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateTagCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse<TagVm>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Product.GetById(request.ProductSku.FromHashId());
        
        if (product == null)
        {
            throw new NotFoundException("Product", request.ProductSku);
        }
        
        if (product.Tags.Any(_ => _.Name == request.Name))
        {
            throw new CustomException($"Tag name '{request.Name}' is in used by the product '{request.ProductSku}'", errorStatus: ErrorStatus.Validation);
        }
        
        var entity = _mapper.Map<Tag>(request);

        _unitOfWork.Tag.Add(entity);

        await _unitOfWork.SaveAsync(cancellationToken);

        var response = new ApiResponse<TagVm>
        {
            Data = _mapper.Map<TagVm>(entity)
        };

        return response;
    }
}