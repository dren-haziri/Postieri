using Postieri.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Postieri.DTOs
{
    public class ShelfDto
    {
        public int ShelfId { get; set; }
        public int WarehouseId { get; set; }
        public string BinLetter { get; set; }
        public int AvailableSlots { get; set; }
    }
    public class ShelfProductsDto
    {
        public int ShelfId { get; set; }
        public int WarehouseId { get; set; }
        public string BinLetter { get; set; }
        public int AvailableSlots { get; set; }
        public List<ProductDto> Products { get; set; }
    }

    public class ShelfWarehouseDto
    {
        public int ShelfId { get; set; }
      
        public string BinLetter { get; set; }
        public int AvailableSlots { get; set; }
        public int WarehouseId { get; set; }

        public WarehouseDto Warehouse { get; set; }

        public List<ProductDto> Products { get; set; }

    }

    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Price { get; set; }
        public int ShelfId  { get; set; }

    }
}
