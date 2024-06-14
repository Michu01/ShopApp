using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Products.Models;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Products.Queries.GetProducts;

public record GetProductsQuery(int Page = 1, int Limit = 30, string? Search = null) : IRequest<PaginatedResult<Product>>;

public class GetProductsQueryHandler(IApplicationDbContext dbContext, IMapper mapper) : IRequestHandler<GetProductsQuery, PaginatedResult<Product>>
{
    public async Task<PaginatedResult<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext
            .Products
            .AsNoTracking();

        if (!string.IsNullOrEmpty(request.Search))
        {
            var words = request.Search.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            query = query.Where(product => words.Any(word => product.Name.Contains(word)));
        }
            
        var result = await query
            .OrderByDescending(e => e.CreatedAt)
            .ProjectTo<Product>(mapper.ConfigurationProvider)
            .ToPaginatedResult(request.Page, request.Limit, cancellationToken);

        return result;
    }
}
