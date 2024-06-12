using Application.Common.Interfaces;
using Application.Common.Results;

using Domain.Entities;

using FluentResults;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest<Result>;

public class DeleteProductCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<DeleteProductCommand, Result>
{
    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext
            .Products
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (entity is null)
        {
            return Result.Fail(NotFoundResult.Create<ProductEntity>(request.Id));
        }

        dbContext.Products.Remove(entity);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
