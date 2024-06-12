
using FluentValidation;

namespace Api.Filters;

public class ExceptionFilter(ILogger<ExceptionFilter> logger) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        try
        {
            return await next(context);
        }
        catch (ValidationException ex)
        {
            return Results.BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError("{Message}", ex.Message);

            return Results.Problem("Internal server error", statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}
