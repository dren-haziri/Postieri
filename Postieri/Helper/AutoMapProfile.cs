using AutoMapper;
using Postieri.Models;

namespace Postieri.Helper
{
    public class AutoMapProfile : Profile
    {
        public AutoMapProfile()
        {
          
            CreateMap<Shelf, ShelfDto>().ReverseMap();
            CreateMap<Warehouse, WarehouseDto>().ReverseMap();
            CreateMap<Shelf, ShelfWarehouseDto>();
            CreateMap<Warehouse, WarehouseShelvesDto>();
          
            


        }
    }

}
