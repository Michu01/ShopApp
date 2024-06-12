using Application.Products.Models;
using AutoMapper;

using Domain.Entities;

namespace Application.Products.MapperProfiles;

internal class ProductDetailsMapperProfile : Profile
{
    public ProductDetailsMapperProfile()
    {
        CreateMap<ProductEntity, ProductDetails>();
    }
}
