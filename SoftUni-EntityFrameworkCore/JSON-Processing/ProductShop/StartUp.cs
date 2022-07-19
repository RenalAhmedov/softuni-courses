using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProductShop.Data;
using ProductShop.DataTransferObjects;
using ProductShop.Dtos;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new ProductShopContext();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            //var inputJson = File.ReadAllText("../../../Datasets/users.json");
            //var inputJson = File.ReadAllText("../../../Datasets/products.json");
            //var inputJson = File.ReadAllText("../../../Datasets/categories.json");
            //var inputJson = File.ReadAllText("../../../Datasets/categories-products.json");

            Console.WriteLine(GetUsersWithProducts(context));
        }

        public static IMapper InitializeMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });

            return config.CreateMapper();
        }

        //01->
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var mapper = InitializeMapper();
            var users = JsonConvert.DeserializeObject<ICollection<UserDTO>>(inputJson);

            var mappedUsers = mapper.Map<ICollection<User>>(users);
            context.Users.AddRange(mappedUsers);
            context.SaveChanges();

            return $"Successfully imported {mappedUsers.Count}";
        }
        //<-

        //02->
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var mapper = InitializeMapper();
            var products = JsonConvert.DeserializeObject<ICollection<ProductDTO>>(inputJson);

            var mappedProducts = mapper.Map<ICollection<Product>>(products);

            context.Products.AddRange(mappedProducts);
            context.SaveChanges();

            return $"Successfully imported {mappedProducts.Count}";
        }
        //<-

        //03->
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var mapper = InitializeMapper();
            var categories = JsonConvert.DeserializeObject<ICollection<CategoriesDTO>>(inputJson)
                .Where(x => !string.IsNullOrEmpty(x.Name));
;

            var mappedCategories = mapper.Map<ICollection<Category>>(categories);

            context.Categories.AddRange(mappedCategories);
            context.SaveChanges();

            return $"Successfully imported {mappedCategories.Count}";
        }
        //<-

        //04->
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var mapper = InitializeMapper();
            var categoryProducts = JsonConvert.DeserializeObject<ICollection<CategoryProductDTO>>(inputJson);

            var mappedCategoryProducts = mapper.Map<ICollection<CategoryProduct>>(categoryProducts);
            context.CategoryProducts.AddRange(mappedCategoryProducts);
            context.SaveChanges();

            return $"Successfully imported {mappedCategoryProducts.Count}";
        }
        //<-

        //05->
        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(x => x.Price >= 500 && x.Price <= 1000)
                .Select(x => new
                {
                    x.Name,
                    x.Price,
                    Seller = x.Seller.FirstName + " " + x.Seller.LastName
                })
                .OrderBy(x => x.Price)
                .ToList();

            var options = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };

            var jsonProducts = JsonConvert.SerializeObject(products, options);
            return jsonProducts;
        }
        //<-

        //06->
        public static string GetSoldProducts(ProductShopContext context)
        {
            var soldProducts = context.Users
                .Where(x => x.ProductsSold.Count >= 1 && x.ProductsSold.Any(b => b.Buyer != null))
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    SoldProducts = x.ProductsSold.Where(b => b.Buyer != null).Select(p => new
                    {
                        p.Name,
                        p.Price,
                        BuyerFirstName = p.Buyer.FirstName,
                        BuyerLastName = p.Buyer.LastName
                    })
                })
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .ToList();

            var options = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };

            var jsonSoldProducts = JsonConvert.SerializeObject(soldProducts, options);
            return jsonSoldProducts;
        }
        //<-

        //07->
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var products = context.Categories
                .OrderByDescending(x => x.CategoryProducts.Count)
                .Select(x => new
                {
                    Category = x.Name,
                    ProductsCount = x.CategoryProducts.Count,
                    AveragePrice = x.CategoryProducts.Average(p => p.Product.Price).ToString("f2"),
                    TotalRevenue = x.CategoryProducts.Sum(p => p.Product.Price).ToString("f2")
                })
                .ToList();

            var options = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };

            var jsonCategories = JsonConvert.SerializeObject(products, options);

            return jsonCategories;
        }
        //<-

        //08->
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(x => x.ProductsSold.Count >= 1 && x.ProductsSold.Any(b => b.Buyer != null))
                .Include(x => x.ProductsSold)
                .ToList()
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.Age,
                    SoldProducts = new
                    {
                        Count = x.ProductsSold.Where(b => b.Buyer != null).Count(),
                        Products = x.ProductsSold.Where(p => p.Buyer != null).Select(p => new
                        {
                            p.Name,
                            p.Price
                        })
                    }
                })
                .OrderByDescending(x => x.SoldProducts.Count)
                .ToList();

            var options = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };

            var result = new
            {
                usersCount = users.Count,
                Users = users
            };

            var jsonUsers = JsonConvert.SerializeObject(result, options);

            return jsonUsers;
        }
        //<-
    }
}