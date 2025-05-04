using AutoMapper;
using OrdersAPI.Entities;
using OrdersAPI.Models;

namespace OrdersAPI.Mappings
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<CreateOrderItemDto, OrderItem>();

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(x => x.Name, dto => dto.MapFrom(x => x.Product.Name))
                .ForMember(x => x.Description, dto => dto.MapFrom(x => x.Product.Description));

            CreateMap<Order, OrderDto>();
        }
    }
}