namespace Postieri.Models
{
    public class Shelf
    {
        public int ShelfId { get; set; }
        public int ShelfSizeId { get; set; }
        public ShelfSize?  Size { get; set; }

        public int WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }

        public char BinLetter { get; set; }
        public int MaxProducts { get; set; }
        public List<Product>? Products { get; set; }
    }
}
