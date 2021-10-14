using AutoMapper;
using ProductsBusinessLayer.DTOs;
using ProductsCore.Models;
using System;

namespace ProductsBusinessLayer.MapperProfiles
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            CreateMap<ProductDTO, Product>()
                .ForMember(x => x.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(x => x.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(x => x.Category, opt => opt.MapFrom(src => ToCategory(src.Category)))
                .ForMember(x => x.IsAvailableToBuy, opt => opt.MapFrom(src => true))
                .ForMember(x => x.Id, opt => opt.Ignore());
        }

        private Category ToCategory(string category)
        {
            return Enum.TryParse(typeof(Category), category, out var result)
                ? (Category)result
                : default;
        }
    }
}
