namespace KidsRUs.Application.Handlers.Categories.Queries.GetCategory;

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, ApiResponse<CategoryVm>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCategoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ApiResponse<CategoryVm>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var entity = _unitOfWork.Category.GetById(request.Sku.FromHashId());

        if (entity == null)
        {
            throw new NotFoundException("Category", request.Sku);
        }
        
        return new ApiResponse<CategoryVm>()
        {
            Data = _mapper.Map<CategoryVm>(entity)
        };
    }
}