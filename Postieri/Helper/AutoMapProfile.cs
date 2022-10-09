using AutoMapper;
using Postieri.Models;

namespace Postieri.Helper
{
    public class AutoMapProfile : Profile
    {
        public AutoMapProfile()
        {
            //CreateMap<Shelf, WarehouseDto>();

            //CreateMap<Shelf, ShelfWarehouseDto>().ForMember(d => d.Warehouse.Name,
            //      opt => opt.MapFrom(s => s));

            // CreateMap<Warehouse, WarehouseDto>().ForMember(d => d.ShelvesId, o => o.MapFrom(s => s.Shelves.Select(m => m.ShelfId))); 

            CreateMap<Shelf, ShelfDto>().ReverseMap();
            CreateMap<Warehouse, WarehouseDto>().ReverseMap();
            CreateMap<Shelf, ShelfWarehouseDto>();
            CreateMap<Warehouse, WarehouseShelvesDto>();
          
            


        }
    }

}
