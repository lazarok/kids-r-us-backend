namespace KidsRUs.Application.Handlers.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ApiResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Product.GetById(request.Sku.FromHashId());
        
        if (entity == null)
        {
            throw new NotFoundException("Product", request.Sku);
        }

        _unitOfWork.Product.Remove(entity);

        await _unitOfWork.SaveAsync(cancellationToken);
        
        return new ApiResponse();
    }
}