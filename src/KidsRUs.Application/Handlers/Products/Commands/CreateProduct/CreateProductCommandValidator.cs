namespace KidsRUs.Application.Handlers.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateProductCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        
        RuleFor(_ => _.Name)
            .NotNull().WithMessage("'{PropertyName}' is required.")
            .NotEmpty().WithMessage("'{PropertyName}' is empty.")
            .MaximumLength(50).WithMessage("'{PropertyName}' must have maximum 50 characters.")
            .MustAsync(IsUniqueName).WithMessage("'{PropertyName}' already exists.");
        
        RuleFor(_ => _.Description)
            .NotNull().WithMessage("'{PropertyName}' is required.")
            .NotEmpty().WithMessage("'{PropertyName}' is empty.")
            .MaximumLength(100).WithMessage("'{PropertyName}' must have maximum 100 characters.");
        
        RuleFor(x => x.ProductStock).Must(ProductStockGreaterThanZero).WithMessage("'{PropertyName}' must be greater than 0");
        
        RuleFor(x => x.Price).Must(PriceGreaterThanZero).WithMessage("'{PropertyName}' must be greater than 0");
        
        RuleFor(x => x.AverageRating).Must(AverageRating).WithMessage("'{PropertyName}' must be between 1 and 5");
        
        RuleFor(x => x.Tags)
            .Must(NotRepeatTags).WithMessage("'{PropertyName}' there are repeated tags")
            .Must(LengthTags).WithMessage("'{PropertyName}' must have maximum 30 characters.");
        
        RuleFor(x => x.CategorySku)
            .NotNull().WithMessage("'{PropertyName}' is required.")
            .NotEmpty().WithMessage("'{PropertyName}' is empty.")
            .MustAsync(ExistsCategory).WithMessage("'{PropertyName}' not found");
    }
    
    private async Task<bool> IsUniqueName(string name, CancellationToken cancellationToken)
    {
        return !_unitOfWork.Product.Any(_ => _.Name == name);
    }
    
    private async Task<bool> ExistsCategory(string categoryKsu, CancellationToken cancellationToken)
    {
        return _unitOfWork.Category.Any(_ => _.Id == categoryKsu.FromHashId());
    }
    
    private bool NotRepeatTags(List<string>? tags)
    {
        if (tags == null)
        {
            return true;
        }

        var dictionary = new Dictionary<string, bool>();

        foreach (var tag in tags)
        {
            if (dictionary.ContainsKey(tag))
            {
                return false;
            }
            
            dictionary.Add(tag,false);
        }

        return true;
    }
    
    private bool LengthTags(List<string>? tags)
    {
        return tags == null || tags.All(tag => tag.Length <= 30);
    }
    
    private bool ProductStockGreaterThanZero(int productStock) =>  productStock > 0;

    private bool PriceGreaterThanZero(decimal price) =>  price > 0;
    
    private bool AverageRating(decimal rating) =>  rating >= 1 && rating <=5;
}