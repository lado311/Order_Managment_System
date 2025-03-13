using AutoMapper;
using ProductApi.Application.DTOs;
using ProductApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Application.Mapping
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();

        }
    }
}
