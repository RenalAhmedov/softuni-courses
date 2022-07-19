using AutoMapper;
using ProductShop.DataTransferObjects;
using ProductShop.Dtos;
using ProductShop.Models;

namespace ProductShop
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            this.CreateMap<UserDTO, User>();
            this.CreateMap<ProductDTO, Product>();
            this.CreateMap<CategoriesDTO, Category>();
            this.CreateMap<CategoryProductDTO, CategoryProduct>();
        }
    }
}
