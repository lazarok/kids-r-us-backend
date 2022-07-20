namespace KidsRUs.Application.Handlers.Tags.Commands.DeleteTag;

public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand, ApiResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTagCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var entity = _unitOfWork.Tag.GetById(request.Sku.FromHashId());
        
        if (entity == null)
        {
            throw new NotFoundException("Tag", request.Sku);
        }

        _unitOfWork.Tag.Remove(entity);

        await _unitOfWork.SaveAsync(cancellationToken);
        
        return new ApiResponse();
    }
}