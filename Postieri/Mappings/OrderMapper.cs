using Postieri.Models;
using AutoMapper;

namespace Postieri.Mappings
{
    public class OrderMapper : Profile

    {
        public OrderMapper()
        {
            CreateMap<Order, Product>().ReverseMap();
        }
    }
}
