using Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<ProductEntity> Products { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
