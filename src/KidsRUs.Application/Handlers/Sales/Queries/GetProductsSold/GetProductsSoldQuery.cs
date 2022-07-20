namespace KidsRUs.Application.Handlers.Sales.Queries.GetProductsSold;

public class GetProductsSoldQuery : ProductSoldFilter, IRequest<PaginationResponse<ProductVm>> {}