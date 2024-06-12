using Domain.Common;

using FluentResults;

namespace Application.Common.Results;

public class NotFoundResult(string entityName, Guid id) : Error($"{entityName} with id {id} not found")
{
    public static NotFoundResult Create<T>(Guid id) where T : Entity => new(typeof(T).Name, id);
}
