using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiSimpleProject.Data.Models;
using WebApiSimpleProject.Models;

namespace WebApiSimpleProject.AutoMapperProfiles
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<ProductCategory, ProductCategoryDTO>();
            CreateMap<ProductCategoryDTO, ProductCategory>();

            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();
        }
    }
}
