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

        public Product ProductId {get;set;}

        public DateTime Date {get;set;}

        public User CourierId {get;set;}//courier

        public User ManagerId {get;set;}

        public double Price {get;set;}
    }
}