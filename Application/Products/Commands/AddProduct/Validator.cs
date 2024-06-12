using Application.Products.Validators;

using FluentValidation;

namespace Application.Products.Commands.AddProduct;

public class Validator : AbstractValidator<AddProductCommand>
{
    public Validator()
    {
        RuleFor(e => e.Product)
            .SetValidator(new CreateProductValidator());
    }
}
