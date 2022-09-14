namespace Postieri.DTOs
{
    public class ShelfDto
    {
        public int ShelfId { get; set; }
        public int WarehouseId { get; set; }
        public char BinLetter { get; set; }
        public int MaxProducts { get; set; }
    }
}
