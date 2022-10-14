namespace Postieri.Models
{
    public class Shelf
    {
        public int ShelfId { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }
        public List<Product>? Products { get; set; }

        public char BinLetter { get; set; }
        public int AvailableSlots { get; set; }     


    }
}
