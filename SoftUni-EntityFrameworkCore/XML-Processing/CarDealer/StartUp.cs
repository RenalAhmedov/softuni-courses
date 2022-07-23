using AutoMapper;
using CarDealer.Data;
using CarDealer.Dtos.Export;
using CarDealer.Dtos.Import;
using CarDealer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new CarDealerContext();
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            //var xmlInput = File.ReadAllText("../../../Datasets/suppliers.xml");
            //var xmlInput = File.ReadAllText("../../../Datasets/parts.xml");
            //var xmlInput = File.ReadAllText("../../../Datasets/cars.xml");
            //var xmlInput = File.ReadAllText("../../../Datasets/customers.xml");
            //var xmlInput = File.ReadAllText("../../../Datasets/sales.xml");

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
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var mapper = InitializeMapper();
            var serializer = new XmlSerializer(typeof(SupplierDto[]), new XmlRootAttribute("Suppliers"));

            var supplierDtos = (SupplierDto[])serializer.Deserialize(new StringReader(inputXml));
            var suppliers = mapper.Map<Supplier[]>(supplierDtos);

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Length}";
        }
        //<-

        //10->
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            var mapper = InitializeMapper();
            var serializer = new XmlSerializer(typeof(PartDto[]), new XmlRootAttribute("Parts"));
            var parts = new HashSet<Part>();

            var existingSuppliers = context.Suppliers
                .Select(x => x.Id)
                .ToHashSet();

            var partsDto = (PartDto[])serializer.Deserialize(new StringReader(inputXml));

            foreach (var partDto in partsDto)
            {
                if (existingSuppliers.Contains(partDto.SupplierId))
                {
                    var part = mapper.Map<Part>(partDto);
                    parts.Add(part);
                }
            }

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Count}";
        }
        //<-

        //11->
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            var mapper = InitializeMapper();
            var serializer = new XmlSerializer(typeof(CarDto[]), new XmlRootAttribute("Cars"));

            var carsDto = (CarDto[])serializer.Deserialize(new StringReader(inputXml));
            var cars = new HashSet<Car>();

            foreach (var carDto in carsDto)
            {
                var car = mapper.Map<Car>(carDto);

                foreach (var partId in carDto.Parts.Select(x => x.Id).Distinct())
                {
                    var part = context.Parts.Where(x => x.Id == partId).FirstOrDefault();

                    if (part != null)
                    {
                        car.PartCars.Add(new PartCar
                        {
                            Part = part,
                            Car = car
                        });
                    }
                }

                cars.Add(car);
            }
           ;
            context.Cars.AddRange(cars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count}";
        }
        //<-

        //12->
        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            var mapper = InitializeMapper();
            var serializer = new XmlSerializer(typeof(CustomerDto[]), new XmlRootAttribute("Customers"));
            var customersDto = (CustomerDto[])serializer.Deserialize(new StringReader(inputXml));

            var customers = mapper.Map<Customer[]>(customersDto);
            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Count()}";
        }
        //<-

        //13->
        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            var mapper = InitializeMapper();
            var serializer = new XmlSerializer(typeof(SaleDto[]), new XmlRootAttribute("Sales"));

            var salesDto = (SaleDto[])serializer.Deserialize(new StringReader(inputXml));
            var sales = new HashSet<Sale>();

            foreach (var saleDto in salesDto)
            {
                var car = context.Cars.FirstOrDefault(x => x.Id == saleDto.CarId);

                if (car != null)
                {
                    var sale = mapper.Map<Sale>(saleDto);
                    sales.Add(sale);
                }
            }
            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count}";
        }
        //<-

        //14->
        public static string GetCarsWithDistance(CarDealerContext context)
        {
            var serializer = new XmlSerializer(typeof(CarWithDistanceDto[]), new XmlRootAttribute("cars"));
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var cars = context.Cars
                .Where(x => x.TravelledDistance > 2000000)
                .Select(x => new CarWithDistanceDto
                {
                    Make = x.Make,
                    Model = x.Model,
                    TravelledDistance = x.TravelledDistance
                })
                .OrderBy(x => x.Make)
                .ThenBy(x => x.Model)
                .Take(10)
                .ToArray();
            ;
            serializer.Serialize(sw, cars, namespaces);

            return sb.ToString();
        }
        //<-

        //15->
        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            var serializer = new XmlSerializer(typeof(CarFromMakeBmwDto[]), new XmlRootAttribute("cars"));
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var cars = context.Cars
                .Where(x => x.Make == "BMW")
                .Select(x => new CarFromMakeBmwDto
                {
                    Id = x.Id,
                    Model = x.Model,
                    TravelledDistance = x.TravelledDistance
                })
                .OrderBy(x => x.Model)
                .ThenByDescending(x => x.TravelledDistance)
                .ToArray();

            serializer.Serialize(sw, cars, namespaces);

            return sb.ToString();
        }
        //<-

        //16->
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var serializer = new XmlSerializer(typeof(LocalSupplierDto[]), new XmlRootAttribute("suppliers"));
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var suppliers = context.Suppliers
                .Where(x => !x.IsImporter)
                .Select(x => new LocalSupplierDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    PartsCount = x.Parts.Count
                })
                .ToArray();

            serializer.Serialize(sw, suppliers, namespaces);

            return sb.ToString();
        }
        //<-

        //17->
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var serializer = new XmlSerializer(typeof(CarWithPartsDto[]), new XmlRootAttribute("cars"));
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var cars = context.Cars
                .Select(x => new CarWithPartsDto
                {
                    Make = x.Make,
                    Model = x.Model,
                    TravelledDistance = x.TravelledDistance,
                    Parts = x.PartCars
                    .Where(c => c.CarId == x.Id)
                    .Select(p => new ExportPartDto
                    {
                        Name = p.Part.Name,
                        Price = p.Part.Price
                    })
                    .OrderByDescending(x => x.Price)
                    .ToArray()
                })
                .OrderByDescending(x => x.TravelledDistance)
                .ThenBy(x => x.Model)
                .Take(5)
                .ToArray();

            serializer.Serialize(sw, cars, namespaces);

            return sb.ToString();
        }
        //<-

        //18->
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var serializer = new XmlSerializer(typeof(ExportCustomerDto[]), new XmlRootAttribute("customers"));
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var customers = context.Customers
                .Where(x => x.Sales.Count > 0)
                .Select(x => new ExportCustomerDto
                {
                    FullName = x.Name,
                    BoughtCars = x.Sales.Count,
                    SpentMoney = x.Sales
                    .Select(c => c.Car)
                    .SelectMany(p => p.PartCars)
                    .Sum(p => p.Part.Price)
                })
                .OrderByDescending(x => x.SpentMoney)
                .ToArray();

            serializer.Serialize(sw, customers, namespaces);

            return sb.ToString();
        }
        //<-

        //19->
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var serializer = new XmlSerializer(typeof(ExportSaleDto[]), new XmlRootAttribute("sales"));
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var sales = context.Sales
                .Select(x => new ExportSaleDto
                {
                    Car = new SoldCarDto
                    {
                        Make = x.Car.Make,
                        Model = x.Car.Model,
                        TravelledDistance = x.Car.TravelledDistance
                    },
                    Discount = x.Discount,
                    CustomerName = x.Customer.Name,
                    Price = x.Car.PartCars.Sum(p => p.Part.Price),
                    PriceWithDiscount = ((x.Car.PartCars.Select(p => p.Part.Price).Sum()) * (100 - x.Discount) / 100)
                })
                .ToArray();

            serializer.Serialize(sw, sales, namespaces);

            return sb.ToString();
        }
        //<-
    }
}