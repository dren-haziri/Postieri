using System.ComponentModel.DataAnnotations;

namespace Postieri.Models
{
    public class ClientOrder
    {
        [Key]
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public DateTime Date { get; set; }
        public DateTime OrderedOn { get; set; }
        public double Price { get; set; }
        public string CompanyToken { get; set; }
        // public string Cilent { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
    }
}
