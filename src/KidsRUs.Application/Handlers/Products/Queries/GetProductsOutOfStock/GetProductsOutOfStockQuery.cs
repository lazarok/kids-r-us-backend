namespace KidsRUs.Application.Handlers.Products.Queries.GetProductsOutOfStock;

public class GetProductsOutOfStockQuery : ProductsOutOfStockFilter, IRequest<PaginationResponse<ProductVm>> {}