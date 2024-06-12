using Application.Common.Interfaces;
using Application.Common.Results;
using Application.Products.Models;

using AutoMapper;

using Domain.Entities;

using FluentResults;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Products.Queries.GetProduct;

public record GetProductQuery(Guid Id) : IRequest<Result<ProductDetails>>;

public class GetProductQueryHandler(IApplicationDbContext dbContext, IMapper mapper) : IRequestHandler<GetProductQuery, Result<ProductDetails>>
{
    public async Task<Result<ProductDetails>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await dbContext
            .Products
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (product is null)
        {
            return Result.Fail(NotFoundResult.Create<ProductEntity>(request.Id));
        }

        return mapper.Map<ProductDetails>(product);
    }
}
