using FluentValidation;
using ProductsService.BusinessLogic.Dtos;

namespace ProductsService.BusinessLogic.Validators;

public class ProductUpdateRequestValidator : AbstractValidator<ProductUpdateRequest>
{
    public ProductUpdateRequestValidator()
    {
        RuleFor(x => x.ProductID)
            .NotEmpty().WithMessage("ProductID is required");

        RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage("Name is required");

        RuleFor(x => x.Category)
            .IsInEnum().WithMessage("Invalid category");

        RuleFor(x => x.UnitPrice)
            .InclusiveBetween(0, double.MaxValue).WithMessage("Unit price out of range");

        RuleFor(x => x.QuantityInStock)
            .InclusiveBetween(0, int.MaxValue).WithMessage("Quantity out of range");
    }
}
