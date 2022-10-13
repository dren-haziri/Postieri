using Postieri.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Postieri.DTOs
{
    public class ShelfDto
    {
        public int ShelfId { get; set; }
        public int WarehouseId { get; set; }
        public char BinLetter { get; set; }
        public int MaxProducts { get; set; }
    }

    public class ShelfWarehouseDto
    {
        public int ShelfId { get; set; }
      
        public char BinLetter { get; set; }
        public int MaxProducts { get; set; }
        public int WarehouseId { get; set; }

        public WarehouseDto Warehouse { get; set; }



    }
}
