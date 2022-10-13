using Postieri.Models;
using AutoMapper;
using Postieri.DTO;

namespace Postieri.Mappings
{
    public class OrderMapper : Profile

    {
        public OrderMapper()
        {
            CreateMap<Order, Product>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
        }
    }
}
