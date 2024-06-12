using Application.Products.Models;
using AutoMapper;

using Domain.Entities;

namespace Application.Products.MapperProfiles;

internal class CreateProductMapperProfile : Profile
{
    public CreateProductMapperProfile()
    {
        CreateMap<CreateProduct, ProductEntity>();
    }
}
