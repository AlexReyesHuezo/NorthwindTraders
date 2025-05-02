using AutoMapper;
using NorthwindTraders.Application.DTOs;
using NorthwindTraders.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NorthwindTraders.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Order -> OrderDto
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.OrderDetail, opt => opt.MapFrom(src => src.OrderDetail));

            // OrderDto -> Order
            CreateMap<OrderDto, Order>()
                .ForMember(dest => dest.OrderDetail, opt => opt.Ignore()); // We handle order details separately

            // OrderDetail -> OrderDetailDto
            CreateMap<OrderDetail, OrderDetailDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.ProductName : string.Empty));

            // OrderDetailDto -> OrderDetail
            CreateMap<OrderDetailDto, OrderDetail>();

            // Customer mappings
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();

            // Employee mappings
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
            CreateMap<EmployeeDto, Employee>();

            // Product mappings
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
        }
    }
}