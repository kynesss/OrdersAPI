using AutoMapper;
using OrdersAPI.Entities;
using OrdersAPI.Models;

namespace OrdersAPI.Mappings
{
    public class AccountMappingProfile : Profile
    {
        public AccountMappingProfile()
        {
            CreateMap<RegisterDto, User>()
                .ForMember(x => x.UserName, x => x.MapFrom(x => x.Email));
        }
    }
}