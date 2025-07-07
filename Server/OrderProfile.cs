using AutoMapper;
using Server.Models;
using Server.Models.DTO;

namespace Server
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderDTO, Order>();
            CreateMap<Order, OrderDTO>();
        }
    }
}
