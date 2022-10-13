using System.ComponentModel.DataAnnotations;

namespace Postieri.Models
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}