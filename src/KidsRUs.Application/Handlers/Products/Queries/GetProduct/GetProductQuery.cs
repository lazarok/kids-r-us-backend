namespace KidsRUs.Application.Handlers.Products.Queries.GetProduct;

public record GetProductQuery(string Sku) : IRequest<ApiResponse<ProductVm>> {}