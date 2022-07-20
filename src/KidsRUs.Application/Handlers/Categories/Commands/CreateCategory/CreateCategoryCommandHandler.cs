namespace KidsRUs.Application.Handlers.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ApiResponse<CategoryVm>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse<CategoryVm>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Category>(request);

        _unitOfWork.Category.Add(entity);

        await _unitOfWork.SaveAsync(cancellationToken);

        var response = new ApiResponse<CategoryVm>
        {
            Data = _mapper.Map<CategoryVm>(entity)
        };

        return response;
    }
}