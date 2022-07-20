namespace KidsRUs.Application.Handlers.Sales.Commands.CreateSale;

public class CreateSaleCommand : IRequest<ApiResponse>
{
    public string ProductSku { get; set; }
}