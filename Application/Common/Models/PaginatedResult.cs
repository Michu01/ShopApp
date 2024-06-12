namespace Application.Common.Models;

public record PaginatedResult<T>(
    IReadOnlyCollection<T> Items,
    int TotalCount);
