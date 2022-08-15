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
        public Guid OrderId {get;set;}

        public Guid ProductId {get;set;}

        public Guid AddressTo { get; set; }

        public DateTime Date {get;set;}

        public Guid CourierId {get;set;}//courier

        public Guid ManagerId {get;set;}

        public double Price {get;set;}
    }
}