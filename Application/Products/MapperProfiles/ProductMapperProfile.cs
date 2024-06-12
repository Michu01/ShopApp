using Application.Products.Models;
using AutoMapper;

using Domain.Entities;

namespace Application.Products.MapperProfiles;

internal class ProductMapperProfile : Profile
{
    public ProductMapperProfile()
    {
        CreateMap<ProductEntity, Product>();
    }
}
