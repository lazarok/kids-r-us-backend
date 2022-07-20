namespace KidsRUs.Application.Handlers.Sales.Queries.GetProductsSold;

public class GetProductsSoldQueryHandler : IRequestHandler<GetProductsSoldQuery, PaginationResponse<ProductVm>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductsSoldQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginationResponse<ProductVm>> Handle(GetProductsSoldQuery request, CancellationToken cancellationToken)
    {
        var queryable = _unitOfWork.Sale.GetProductsSold(request);
        
        return await queryable.PaginatedListAsync<Product, ProductVm>(_mapper, request.PageNumber, request.PageSize, cancellationToken);
    }
}