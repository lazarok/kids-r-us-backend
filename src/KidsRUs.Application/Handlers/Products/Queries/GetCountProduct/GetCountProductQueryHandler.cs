using Microsoft.EntityFrameworkCore;

namespace KidsRUs.Application.Handlers.Products.Queries.GetCountProduct;

public class GetCountProductQueryHandler : IRequestHandler<GetCountProductQuery, ApiResponse<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCountProductQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ApiResponse<int>> Handle(GetCountProductQuery request, CancellationToken cancellationToken)
    {
        var queryable = _unitOfWork.Product.GetAll(request);

        return new ApiResponse<int>()
        {
            Data = await queryable.CountAsync(cancellationToken: cancellationToken)
        };
    }
}