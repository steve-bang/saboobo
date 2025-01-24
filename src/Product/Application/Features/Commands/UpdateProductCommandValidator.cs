
using FluentValidation;

namespace SaBooBo.Product.Application.Features.Commands;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("The name is required.")
            .MaximumLength(200).WithMessage("The name must not exceed 200 characters.");

        RuleFor(p => p.Price)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .GreaterThan(0).WithMessage("Price must be greater than 0.");
    }
}