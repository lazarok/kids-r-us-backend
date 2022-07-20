namespace KidsRUs.Application.Handlers.Products.Queries.GetAllProduct;

public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, PaginationResponse<ProductVm>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllProductQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginationResponse<ProductVm>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        var queryable = _unitOfWork.Product.GetAll(request);
        
        return await queryable.PaginatedListAsync<Product, ProductVm>(_mapper, request.PageNumber, request.PageSize, cancellationToken);
    }
}