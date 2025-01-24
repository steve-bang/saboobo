
using FluentValidation;

namespace SaBooBo.Product.Application.Features.Commands;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithErrorCode("Category_name_required")
            .WithMessage("Category name is required");

        RuleFor(x => x.IconUrl)
            .Matches(@"(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|gif|png)")
            .When(x => !string.IsNullOrEmpty(x.IconUrl))
            .WithMessage("Invalid Icon Url")
            .WithErrorCode("Category_invalid_icon_url");
    }
}