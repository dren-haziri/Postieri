using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Postieri.Models
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }
        public Product Products { get; set; }
        public DateTime Date { get; set; }
        public DateTime OrderedOn { get; set; }
        public double Price { get; set; }
        public Guid? UserId { get; set; }
        public Guid? CompanyId { get; set; }
        public string Address { get; set; }
        public string? Sign { get; set; }
        public string? Status { get; set; }
        public Guid? CourierId { get; set; }
        public Guid? ManagerId { get; set; }
        public string CompanyToken { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }

    }
}