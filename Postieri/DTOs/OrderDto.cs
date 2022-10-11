using Postieri.Models;

namespace Postieri.DTO
{
    public class OrderDto
    {
        public Guid ProductId { get; set; }
        public DateTime Date { get; set; }
        public DateTime OrderedOn { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        public string JWT { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Price { get; set; }
    }
}
