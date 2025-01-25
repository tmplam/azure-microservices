using eCommerce.Core.Dtos;
using FluentValidation;

namespace eCommerce.Core.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email address format");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .Length(1, 50).WithMessage("Password should be 1 to 50 characters long"); ;

        RuleFor(x => x.PersonName)
            .NotEmpty().WithMessage("PersonName is required")
            .Length(1, 50).WithMessage("Person name should be 1 to 50 characters long");

        RuleFor(request => request.Gender)
            .IsInEnum().WithMessage("Invalid gender option");
    }
}
