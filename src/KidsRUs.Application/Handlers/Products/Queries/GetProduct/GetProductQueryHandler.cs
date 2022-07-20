using KidsRUs.Application.Extensions;
using KidsRUs.Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KidsRUs.Application.Handlers.Products.Queries.GetProduct;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ApiResponse<ProductVm>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUriService _uriService;
    private readonly string _containerName;

    public GetProductQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUriService uriService,
        IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _uriService = uriService;
        _containerName = configuration["ProductServiceOptions:ContainerName"];
    }

    public async Task<ApiResponse<ProductVm>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Product.GetById(request.Sku.FromHashId());

        if (entity == null)
        {
            throw new NotFoundException("Product", request.Sku);
        }

        var response = new ApiResponse<ProductVm>()
        {
            Data = _mapper.Map<ProductVm>(entity)
        };

        response.Data.Images = await _unitOfWork.Image.GetImagesAsync(_uriService, _containerName, entity.Id);

        return response;
    }
}