namespace KidsRUs.Application.Handlers.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, ApiResponse<CategoryVm>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCategoryCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse<CategoryVm>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = _unitOfWork.Category.GetById(request.Sku.FromHashId());
        
        if (entity == null)
        {
            throw new NotFoundException("Category", request.Sku);
        }
        
        if (_unitOfWork.Category.Any(_ => _.Id != entity.Id && _.Name == request.Name))
        {
            throw new CustomException("Category name is in used", errorStatus: ErrorStatus.Validation);
        }

        entity.Name = request.Name;

        await _unitOfWork.SaveAsync(cancellationToken);

        var response = new ApiResponse<CategoryVm>
        {
            Data = _mapper.Map<CategoryVm>(entity)
        };

        return response;
    }
}