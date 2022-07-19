using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using CarDealer.Data;
using CarDealer.DTO;
using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new CarDealerContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            //var inputJson = File.ReadAllText("../../../Datasets/suppliers.json");
            //var inputJson = File.ReadAllText("../../../Datasets/parts.json");
            //var inputJson = File.ReadAllText("../../../Datasets/cars.json");
            //var inputJson = File.ReadAllText("../../../Datasets/cars.json");
            //var inputJson = File.ReadAllText("../../../Datasets/sales.json");

            Console.WriteLine(GetSalesWithAppliedDiscount(context));
        }

        private static IMapper InitializeMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CarDealerProfile>();
            });

            return config.CreateMapper();
        }

        //09->
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var mapper = InitializeMapper();

            var jsonSuppliers = JsonConvert.DeserializeObject(inputJson);

            var suppliersDTO = mapper.Map<IEnumerable<SupplierDTO>>(jsonSuppliers);

            var mappedSuppliers = mapper.Map<IEnumerable<Supplier>>(suppliersDTO);

            context.Suppliers.AddRange(mappedSuppliers);
            context.SaveChanges();

            return $"Successfully imported {mappedSuppliers.Count()}.";
        }
        //<-

        //10->
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var mapper = InitializeMapper();

            var partsDTO = JsonConvert.DeserializeObject<IEnumerable<PartsDTO>>(inputJson)
                .Where(x => context.Suppliers
                .Any(s => s.Id == x.SupplierId));


            var mappedParts = mapper.Map<IEnumerable<Part>>(partsDTO);

            context.Parts.AddRange(mappedParts);
            context.SaveChanges();

            return $"Successfully imported {mappedParts.Count()}.";
        }
        //<-

        //11->
        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var mapper = InitializeMapper();

            var carsDTO = JsonConvert.DeserializeObject<IEnumerable<CarsDTO>>(inputJson).ToArray();
            var carsAdded = new List<Car>();

            foreach (var carDto in carsDTO)
            {
                var currentCar = mapper.Map<Car>(carDto);

                foreach (var part in carDto.PartsId.Distinct())
                {
                    var partCar = new PartCar
                    {
                        PartId = part
                    };

                    currentCar.PartCars.Add(partCar);
                }

                carsAdded.Add(currentCar);
            }

            context.Cars.AddRange(carsAdded);
            context.SaveChanges();

            return $"Successfully imported {carsAdded.Count()}.";
        }
        //<-

        //12->
        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var mapper = InitializeMapper();

            var customerDTO = JsonConvert.DeserializeObject<IEnumerable<CustomerDTO>>(inputJson);
                
            var mappedCustomers = mapper.Map<IEnumerable<Customer>>(customerDTO);

            context.Customers.AddRange(mappedCustomers);
            context.SaveChanges();

            return $"Successfully imported {mappedCustomers.Count()}.";
        }
        //<-

        //13->
        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var mapper = InitializeMapper();

            var salesJson = JsonConvert.DeserializeObject<IEnumerable<SalesDTO>>(inputJson);
            var sales = mapper.Map<IEnumerable<Sale>>(salesJson);

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count()}.";
        }
        //<-

        //14->
        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var customers = context.Customers
                .OrderBy(x => x.BirthDate)
                .ThenBy(x => x.IsYoungDriver)
                .Select(x => new
                {
                    Name = x.Name,
                    BirthDate = x.BirthDate.ToString("dd/MM/yyyy"),
                    IsYoungDriver = x.IsYoungDriver
                })
                .ToArray();

            var options = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            };

            var json = JsonConvert.SerializeObject(customers, options);

            return json;
        }
        //<-

        //15->
        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(c => c.Make == "Toyota")
                .Select(x => new
                 {
                     x.Id,
                     x.Make,
                     x.Model,
                     x.TravelledDistance
                 })
                .OrderBy(cm => cm.Model)
                .ThenByDescending(cd => cd.TravelledDistance)
                .ToList();

            var options = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            };

            var json = JsonConvert.SerializeObject(cars, options);

            return json;
        }
        //<-

        //16->
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(x => x.IsImporter == false)
                .Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name,
                    PartsCount = x.Parts.Count
                })
                .ToList();

            var options = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            };

            var json = JsonConvert.SerializeObject(suppliers, options);

            return json;
        }
        //<-

        //17->
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars
                .Select(x => new
                {
                    car = new
                    {
                        Make = x.Make,
                        Model = x.Model,
                        TravelledDistance = x.TravelledDistance
                    },
                    parts = x.PartCars.Select(p => new
                    {
                        Name = p.Part.Name,
                        Price = p.Part.Price.ToString("f2")
                    })
                })
                .ToArray();

            var json = JsonConvert.SerializeObject(cars, Formatting.Indented);

            return json;
        }
        //<-

        //18->
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Where(x => x.Sales.Any())
                .Select(x => new
                {
                    fullName = x.Name,
                    BoughtCars = x.Sales.Count,
                    SpentMoney = x.Sales.Sum(m => m.Car.PartCars.Sum(p => p.Part.Price))
                })
                .OrderByDescending(x => x.SpentMoney)
                .ToArray();

            var options = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };

            var json = JsonConvert.SerializeObject(customers, options);

            return json;
        }
        //<-

        //19->
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            var sales = context.Sales
                .Include(s => s.Customer)
                .Include(s => s.Car)
                .Select(s => new
                {
                    car = new
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TravelledDistance = s.Car.TravelledDistance
                    },
                    customerName = s.Customer.Name,
                    Discount = $"{s.Discount:f2}",
                    price = $"{s.Car.PartCars.Sum(c => c.Part.Price):f2}",
                    priceWithDiscount = $"{s.Car.PartCars.Sum(c => c.Part.Price) - (s.Car.PartCars.Sum(c => c.Part.Price) * (s.Discount / 100)):f2}"
                })
                .Take(10)
                .ToList();

            string salesJson = JsonConvert.SerializeObject(sales, settings);

            return salesJson;
        }
        //<-
    }
}