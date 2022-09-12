namespace Postieri.ViewModels
{
    public class WarehouseVM
    {
        public int WarehouseId { get; set; }
        public string? Name { get; set; }

        public string? Location { get; set; }
        public double Area { get; set; }
        public int NumOfShelves { get; set; }
    }
}
