using Application.Common.Models;

using Microsoft.EntityFrameworkCore;

namespace Application.Common.Extensions;

internal static class IQueryableExtensions
{
    internal static async Task<PaginatedResult<T>> ToPaginatedResult<T>(this IQueryable<T> query, int page, int limit, CancellationToken cancellationToken)
    {
        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query.Skip((page - 1) * limit).Take(limit).ToArrayAsync(cancellationToken);

        return new(items, totalCount);
    }
}
