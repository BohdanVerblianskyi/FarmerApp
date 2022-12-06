using AutoMapper;
using FarmerApp.Api.DTO;
using FarmerApp.Api.Models;
using FarmerApp.Api.ViewModels;

namespace FarmerApp.Api;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Location, LocationDto>();
        CreateMap<LocationVM, Location>();
        CreateMap<LocationDto, Location>();
        
        CreateMap<MeasurementUnit, ModelTypeDto>();
        CreateMap<Product, ModelTypeDto>();
        CreateMap<OwnResource, ModelTypeDto>();
        CreateMap<ProductType, ModelTypeDto>();
        CreateMap<SpendType, ModelTypeDto>();
        
        CreateMap<Product, ProductDto>();
        CreateMap<ProductDto, Product>();
        
        CreateMap<Spend, SpendDto>();
        CreateMap<SpendDto, Spend>();

        CreateMap<WarehouseReceptionDto, WarehouseReception>();
        CreateMap<WarehouseReception, WarehouseReceptionDto>();

    }
}