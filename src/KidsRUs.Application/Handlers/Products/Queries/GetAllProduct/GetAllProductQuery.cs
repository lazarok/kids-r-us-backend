
namespace KidsRUs.Application.Handlers.Products.Queries.GetAllProduct;

public class GetAllProductQuery : ProductFilter, IRequest<PaginationResponse<ProductVm>> {}