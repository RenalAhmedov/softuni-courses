using AutoMapper;
using CarDealer.Dtos.Import;
using CarDealer.Models;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            this.CreateMap<SupplierDto, Supplier>();

            this.CreateMap<PartDto, Part>();

            this.CreateMap<CarDto, Car>();

            this.CreateMap<CustomerDto, Customer>();

            this.CreateMap<SaleDto, Sale>();
        }
    }
}
