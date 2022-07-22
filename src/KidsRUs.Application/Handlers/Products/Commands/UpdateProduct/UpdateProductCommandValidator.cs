namespace KidsRUs.Application.Handlers.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateProductCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(_ => _.Name)
            .NotNull().WithMessage("'{PropertyName}' is required.")
            .NotEmpty().WithMessage("'{PropertyName}' is empty.")
            .MaximumLength(50).WithMessage("'{PropertyName}' must have maximum 50 characters.");

        RuleFor(_ => _.Description)
            .NotNull().WithMessage("'{PropertyName}' is required.")
            .NotEmpty().WithMessage("'{PropertyName}' is empty.")
            .MaximumLength(100).WithMessage("'{PropertyName}' must have maximum 100 characters.");

        RuleFor(x => x.Price).Must(PriceGreaterThanZero).WithMessage("'{PropertyName}' must be greater than 0");
        
        RuleFor(x => x.AverageRating).Must(AverageRating).WithMessage("'{PropertyName}' must be between 1 and 5");
    }

    private bool PriceGreaterThanZero(decimal price) =>  price > 0;
    
    private bool AverageRating(decimal rating) =>  rating >= 1 && rating <=5;
}