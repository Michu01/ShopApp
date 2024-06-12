namespace Application.Products.Models;

public record CreateProduct(
    string Name,
    decimal Price,
    int Stock,
    string? Description);
