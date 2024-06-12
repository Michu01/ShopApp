namespace Application.Products.Models;

public record ProductDetails(
    Guid Id,
    string Name,
    decimal Price,
    int Stock,
    int Sold,
    string? Description);
