using Application.Common.Interfaces;
using Application.Products.Models;

using AutoMapper;

using Domain.Entities;

using MediatR;

namespace Application.Products.Commands.AddProduct;

public record AddProductCommand(CreateProduct Product) : IRequest<ProductDetails>;

public class AddProductCommandHandler(IApplicationDbContext dbContext, IMapper mapper) : IRequestHandler<AddProductCommand, ProductDetails>
{
    public async Task<ProductDetails> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<ProductEntity>(request.Product);

        dbContext.Products.Add(entity);

        await dbContext.SaveChangesAsync(cancellationToken);

        return mapper.Map<ProductDetails>(entity);
    }
}
