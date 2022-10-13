namespace Postieri.DTOs
{
    public class WarehouseDto
    {
        public int WarehouseId { get; set; }
        public string? Name { get; set; }

        public string? Location { get; set; }
        public double Area { get; set; }
        public int NumOfShelves { get; set; }
    }
    public class WarehouseShelvesDto
    {
        public int WarehouseId { get; set; }
        public string? Name { get; set; }

        public string? Location { get; set; }
        public double Area { get; set; }
        public int NumOfShelves { get; set; }
        public List<ShelfDto> Shelves { get; set; }
    }
}
