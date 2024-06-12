namespace Application.Products.Models;

public record Product(
    Guid Id,
    string Name,
    decimal Price,
    int Stock,
    int Sold);