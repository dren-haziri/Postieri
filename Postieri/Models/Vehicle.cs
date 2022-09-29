namespace Postieri.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public string? PlateNumber { get; set; }
        public double LoadWeight { get; set; }

        //LoadSpace(height,length,width)
        public double Height { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double  LoadSpace { get; set; }

        public bool IsAvailable { get; set; }   
        public bool  HasDefect { get; set; }
        public Guid CourierId { get; set; }

    }
}
