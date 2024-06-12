using Application.Products.Validators;

using FluentValidation;

namespace Application.Products.Commands.UpdateProduct;

public class Validator : AbstractValidator<UpdateProductCommand>
{
    public Validator()
    {
        RuleFor(e => e.Product)
            .SetValidator(new CreateProductValidator());
    }
}
