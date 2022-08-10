namespace Postieri.Models
{
    public class Warehouse
    {
        public int WarehouseId { get; set; }
        public string? Name { get; set; }

        public string? Location { get; set; }
        public double Area { get; set; }
        public int NumShelves { get; set; }

        public List<Shelf>? Shelves { get; set; }
    }
}
