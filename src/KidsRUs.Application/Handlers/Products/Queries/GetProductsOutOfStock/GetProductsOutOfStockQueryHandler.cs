namespace KidsRUs.Application.Handlers.Products.Queries.GetProductsOutOfStock;

public class GetProductsOutOfStockQueryHandler : IRequestHandler<GetProductsOutOfStockQuery, PaginationResponse<ProductVm>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductsOutOfStockQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginationResponse<ProductVm>> Handle(GetProductsOutOfStockQuery request, CancellationToken cancellationToken)
    {
        var queryable = _unitOfWork.Product.GetProductsOutOfStock(request);
        
        return await queryable.PaginatedListAsync<Product, ProductVm>(_mapper, request.PageNumber, request.PageSize, cancellationToken);
    }
}