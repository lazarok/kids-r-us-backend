namespace KidsRUs.Application.Handlers.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, ApiResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = _unitOfWork.Category.GetById(request.Sku.FromHashId());
        
        if (entity == null)
        {
            throw new NotFoundException("Category", request.Sku);
        }

        if (_unitOfWork.Product.Any(_ => _.CategoryId == entity.Id))
        {
            throw new CustomException("Category is in used", errorStatus: ErrorStatus.Validation);
        }

        _unitOfWork.Category.Remove(entity);

        await _unitOfWork.SaveAsync(cancellationToken);
        
        return new ApiResponse();
    }
}