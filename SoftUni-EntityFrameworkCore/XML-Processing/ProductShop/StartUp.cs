using AutoMapper;
using ProductShop.Data;
using ProductShop.Dtos;
using ProductShop.Dtos.Export;
using ProductShop.Dtos.Import;
using ProductShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new ProductShopContext();
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            //var xmlInput = File.ReadAllText("../../../Datasets/users.xml");
            //var xmlInput = File.ReadAllText("../../../Datasets/products.xml");
            //var xmlInput = File.ReadAllText("../../../Datasets/categories.xml.");
            //var xmlInput = File.ReadAllText("../../../Datasets/categories-products.xml.");

            Console.WriteLine(GetUsersWithProducts(context));
        }

        private static IMapper InitializeMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });

            return config.CreateMapper();
        }

        //01->
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(UserDto[]), new XmlRootAttribute("Users"));
            var mapper = InitializeMapper();

            var users = (UserDto[])serializer.Deserialize(new StringReader(inputXml));
            var mappedUsers = mapper.Map<User[]>(users).ToArray();

            context.Users.AddRange(mappedUsers);
            context.SaveChanges();

            return $"Successfully imported {mappedUsers.Count()}";
        }
        //<-

        //02->
        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            var mapper = InitializeMapper();
            var serializer = new XmlSerializer(typeof(ProductDto[]), new XmlRootAttribute("Products"));

            var products = (ProductDto[])serializer.Deserialize(new StringReader(inputXml));
            var mappedProducts = mapper.Map<Product[]>(products);

            context.Products.AddRange(mappedProducts);
            context.SaveChanges();

            return $"Successfully imported {mappedProducts.Count()}";
        }
        //<-

        //03->
        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            var mapper = InitializeMapper();
            var serializer = new XmlSerializer(typeof(CategoryDto[]), new XmlRootAttribute("Categories"));

            var categories = (CategoryDto[])serializer.Deserialize(new StringReader(inputXml));
            var mappedCategories = mapper.Map<Category[]>(categories).Where(x => !string.IsNullOrEmpty(x.Name)).ToArray();

            context.Categories.AddRange(mappedCategories);
            context.SaveChanges();

            return $"Successfully imported {mappedCategories.Count()}";
        }
        //<-

        //04->
        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            var mapper = InitializeMapper();
            var serializer = new XmlSerializer(typeof(CategoryProductDto[]), new XmlRootAttribute("CategoryProducts"));

            var categoryIds = context.Categories.Select(x => x.Id).ToHashSet();
            var productIds = context.Products.Select(x => x.Id).ToHashSet();

            var categoryProductsXml = (CategoryProductDto[])serializer.Deserialize(new StringReader(inputXml));

            var categoryProductsToAdd = new List<CategoryProduct>();

            foreach (var categoryProductDto in categoryProductsXml)
            {
                if (categoryIds.Contains(categoryProductDto.CategoryId) && productIds.Contains(categoryProductDto.ProductId))
                {
                    var categoryProduct = mapper.Map<CategoryProduct>(categoryProductDto);
                    categoryProductsToAdd.Add(categoryProduct);
                }
            }

            context.CategoryProducts.AddRange(categoryProductsToAdd);
            context.SaveChanges();

            return $"Successfully imported {categoryProductsToAdd.Count()}";
        }
        //<-

        //05->
        public static string GetProductsInRange(ProductShopContext context)
        {
            var serializer = new XmlSerializer(typeof(ExportProductDto[]), new XmlRootAttribute("Products"));
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var sb = new StringBuilder();
            var sw = new StringWriter(sb);

            var products = context.Products
                .Where(x => x.Price >= 500 && x.Price <= 1000)
                .Select(x => new ExportProductDto
                {
                    Name = x.Name,
                    Price = x.Price,
                    Buyer = x.Buyer.FirstName + " " + x.Buyer.LastName
                })
                .OrderBy(x => x.Price)
                .Take(10)
                .ToArray();

            serializer.Serialize(sw, products, namespaces);

            return sb.ToString();
        }
        //<-

        //06->
        public static string GetSoldProducts(ProductShopContext context)
        {
            var serializer = new XmlSerializer(typeof(ExportUserDto[]), new XmlRootAttribute("Users"));
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var sb = new StringBuilder();
            var sw = new StringWriter(sb);

            var users = context.Users
                .Where(x => x.ProductsSold.Count() >= 1)
                .Select(x => new ExportUserDto
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    SoldProducts = x.ProductsSold.Select(p => new ExportProductDto
                    {
                        Name = p.Name,
                        Price = p.Price
                    })
                    .ToArray()
                })
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .Take(5)
                .ToArray();

            serializer.Serialize(sw, users, namespaces);

            return sb.ToString();
        }
        //<-

        //07->
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var serializer = new XmlSerializer(typeof(ExportCategoryDto[]), new XmlRootAttribute("Categories"));
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);


            var categories = context.Categories
                .Select(x => new ExportCategoryDto
                {
                    Name = x.Name,
                    Count = x.CategoryProducts.Count(),
                    AveragePrice = x.CategoryProducts.Average(p => p.Product.Price),
                    TotalRevenue = x.CategoryProducts.Sum(p => p.Product.Price)
                })
                .OrderByDescending(x => x.Count)
                .ThenBy(x => x.TotalRevenue)
                .ToArray();

            serializer.Serialize(sw, categories, namespaces);

            return sb.ToString();
        }
        //<-

        //08->
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var serializer = new XmlSerializer(typeof(ExportFinalUsersWithProducts), new XmlRootAttribute("Users"));
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var users = context.Users
                .Where(x => x.ProductsSold.Count() >= 1)
                .ToArray()
                .Select(x => new UsersWithProductsDto
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Age = x.Age,
                    SoldProducts = new UsersWithProductsArray
                    {
                        Count = x.ProductsSold.Count,
                        Products = x.ProductsSold.Select(p => new ExportProductDto
                        {
                            Name = p.Name,
                            Price = p.Price
                        })
                        .OrderByDescending(p => p.Price)
                        .ToArray()
                    }
                })
                .OrderByDescending(x => x.SoldProducts.Count);

            var finalUsersDto = new ExportFinalUsersWithProducts
            {
                Count = users.Count(),
                Users = users.Take(10).ToArray()
            };

            serializer.Serialize(sw, finalUsersDto, namespaces);

            return sb.ToString();
        }
        //<-
    }

}