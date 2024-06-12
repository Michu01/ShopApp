using Api.Common;
using Api.Extensions;

using Application.Products.Commands.AddProduct;
using Application.Products.Commands.DeleteProduct;
using Application.Products.Commands.UpdateProduct;
using Application.Products.Models;
using Application.Products.Queries.GetProduct;
using Application.Products.Queries.GetProducts;

using MediatR;

namespace Api.Endpoints;

public class Products : EndpointGroup
{
    public override void Map(IEndpointRouteBuilder builder)
    {
        builder.MapGet("{id}", GetProduct);
        builder.MapGet(string.Empty, GetProducts);
        builder.MapPost(string.Empty, AddProduct);
        builder.MapPut("{id}", UpdateProduct);
        builder.MapDelete("{id}", DeleteProduct);
    }

    private async Task<IResult> GetProduct(IMediator mediator, [AsParameters] GetProductQuery request)
    {
        var result = await mediator.Send(request);

        return result.MapToResponse();
    }

    private async Task<IResult> GetProducts(IMediator mediator, [AsParameters] GetProductsQuery request)
    {
        var result = await mediator.Send(request);

        return Results.Ok(result);
    }

    private async Task<IResult> AddProduct(IMediator mediator, CreateProduct product)
    {
        var request = new AddProductCommand(product);

        var result = await mediator.Send(request);

        return Results.Ok(result);
    }

    private async Task<IResult> UpdateProduct(IMediator mediator, Guid id, CreateProduct product)
    {
        var request = new UpdateProductCommand(id, product);

        var result = await mediator.Send(request);

        return result.MapToResponse();
    }

    private async Task<IResult> DeleteProduct(IMediator mediator, Guid id)
    {
        var request = new DeleteProductCommand(id);

        var result = await mediator.Send(request);

        return result.MapToResponse();
    }
}
