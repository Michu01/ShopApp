using FluentValidation;

namespace Application.Products.Queries.GetProducts;

public class Validator : AbstractValidator<GetProductsQuery>
{
    public Validator()
    {
        RuleFor(e => e.Limit)
            .InclusiveBetween(0, 100);

        RuleFor(e => e.Page)
            .GreaterThanOrEqualTo(1);
    }
}
