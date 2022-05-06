using AutoMapper;
using MarketApp.Dtos.Models;
using MarketApp.Dtos.Request;
using MarketApp.Dtos.Response;
using MarketApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Business.MapperProfile
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UpdateCategoryRequest, Category>();
            CreateMap<Category, GetCategoriesResponse>();
            CreateMap<AddCategoryRequest, Category>();
            
            CreateMap<UpdateProductRequest, Product>();
            CreateMap<Product, GetProductsResponse>();
            CreateMap<AddProductRequest, Product>();

            CreateMap<UserRegisterModel, User>().ForMember(model => model.Password, opt => opt.Ignore());
            CreateMap<User, GetUserResponse>();
            CreateMap<GetUserResponse, User>();

            CreateMap<AddAddressRequest, Address>();
            CreateMap<Address, GetAddressResponse>();

        }
    }
}
