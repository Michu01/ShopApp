using Domain.Common;
using Domain.Constants;

using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class ProductEntity : TimestampedEntity
{
    public Guid Id { get; init; }

    [MaxLength(ProductConstants.MaxNameLength)]
    public required string Name { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public int Sold { get; set; }

    [MaxLength(ProductConstants.MaxDescriptionLength)]
    public string? Description { get; set; }
}
