using Domain.Constants;
using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

internal class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder
            .Property(e => e.Price)
            .HasPrecision(ProductConstants.PricePrecision, ProductConstants.PriceScale);
    }
}
