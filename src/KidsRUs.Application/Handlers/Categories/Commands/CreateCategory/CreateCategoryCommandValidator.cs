namespace KidsRUs.Application.Handlers.Categories.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateCategoryCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        
        RuleFor(_ => _.Name)
            .NotNull().WithMessage("'{PropertyName}' is required.")
            .NotEmpty().WithMessage("'{PropertyName}' is empty.");
        
        RuleFor(_ => _.Name)
            .MustAsync(IsUniqueName)
            .WithMessage("'{PropertyName}' already exists.");
    }
    
    private async Task<bool> IsUniqueName(string name, CancellationToken cancellationToken)
    {
        var tag = _unitOfWork.Category.Find(_ => _.Name == name).FirstOrDefault();
        return tag == default;
    }
}