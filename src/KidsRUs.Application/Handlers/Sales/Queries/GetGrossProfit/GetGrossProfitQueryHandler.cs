namespace KidsRUs.Application.Handlers.Sales.Queries.GetGrossProfit;

public class GetGrossProfitQueryHandler : IRequestHandler<GetGrossProfitQuery, ApiResponse<decimal>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetGrossProfitQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ApiResponse<decimal>> Handle(GetGrossProfitQuery request, CancellationToken cancellationToken)
    {
        var grossProfit = await _unitOfWork.Sale.GetGrossProfit(request);
        return new ApiResponse<decimal>() {Data = grossProfit};
    }
}