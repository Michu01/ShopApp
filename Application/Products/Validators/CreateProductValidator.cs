using Application.Products.Models;
using Domain.Constants;

using FluentValidation;

namespace Application.Products.Validators;

public class CreateProductValidator : AbstractValidator<CreateProduct>
{
    public CreateProductValidator()
    {
        RuleFor(e => e.Price)
            .GreaterThan(0);

        RuleFor(e => e.Stock)
            .GreaterThanOrEqualTo(0);

        RuleFor(e => e.Description)
            .MaximumLength(ProductConstants.MaxDescriptionLength);

        RuleFor(e => e.Name)
            .NotNull()
            .MinimumLength(ProductConstants.MinNameLength)
            .MaximumLength(ProductConstants.MaxNameLength);
    }
}
