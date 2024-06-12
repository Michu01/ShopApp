using Application.Common.Interfaces;
using Application.Common.Results;
using Application.Products.Models;

using AutoMapper;

using Domain.Entities;

using FluentResults;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand(Guid Id, CreateProduct Product) : IRequest<Result<ProductDetails>>;

public class UpdateProductCommandHandler(IApplicationDbContext dbContext, IMapper mapper) : IRequestHandler<UpdateProductCommand, Result<ProductDetails>>
{
    public async Task<Result<ProductDetails>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var existing = await dbContext
            .Products
            .SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (existing is null)
        {
            return Result.Fail(NotFoundResult.Create<ProductEntity>(request.Id));
        }

        mapper.Map(request.Product, existing);

        await dbContext.SaveChangesAsync(cancellationToken);

        return mapper.Map<ProductDetails>(existing);
    }
}