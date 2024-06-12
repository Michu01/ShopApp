using Application.Common.Results;

using FluentResults;

namespace Api.Extensions;

public static class ResultExtensions
{
    public static IResult MapToResponse(this Result result)
    {
        if (result.IsSuccess)
        {
            return Results.Ok();
        }

        var firstError = result.Errors.First();

        if (firstError is NotFoundResult)
        {
            return Results.NotFound(firstError.Message);
        }

        return Results.Problem(title: firstError.Message);
    }

    public static IResult MapToResponse<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return Results.Ok(result.Value);
        }

        return result.ToResult().MapToResponse();
    }
}
