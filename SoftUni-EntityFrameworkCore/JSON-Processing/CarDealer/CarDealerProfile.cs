using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CarDealer.DTO;
using CarDealer.Models;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            this.CreateMap<SupplierDTO, Supplier>();
            this.CreateMap<PartsDTO, Part>();
            this.CreateMap<CarsDTO, Car>();
            this.CreateMap<CustomerDTO, Customer>();
            this.CreateMap<SalesDTO, Sale>();
        }
    }
}
