using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Products.Models;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Products.Queries.GetProducts;

public record GetProductsQuery(int Page = 1, int Limit = 30) : IRequest<PaginatedResult<Product>>;

public class GetProductsQueryHandler(IApplicationDbContext dbContext, IMapper mapper) : IRequestHandler<GetProductsQuery, PaginatedResult<Product>>
{
    public async Task<PaginatedResult<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var result = await dbContext
            .Products
            .AsNoTracking()
            .ProjectTo<Product>(mapper.ConfigurationProvider)
            .ToPaginatedResult(request.Page, request.Limit, cancellationToken);

        return result;
    }
}
