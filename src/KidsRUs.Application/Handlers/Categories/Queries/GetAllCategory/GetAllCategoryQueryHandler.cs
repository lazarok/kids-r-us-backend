namespace KidsRUs.Application.Handlers.Categories.Queries.GetAllCategory;

public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, PaginationResponse<CategoryVm>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllCategoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginationResponse<CategoryVm>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var queryable = _unitOfWork.Category.GetAll();

        if (!string.IsNullOrEmpty(request.Search))
        {
            queryable = queryable.Where(_ => _.Name.ToLower().Contains(request.Search.ToLower()));
        }
        
        return await queryable.PaginatedListAsync<Category, CategoryVm>(_mapper, request.PageNumber, request.PageSize, cancellationToken);
    }
}