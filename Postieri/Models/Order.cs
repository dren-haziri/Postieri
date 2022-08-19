using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Postieri.Models
{
    //order(orderId, productId, date, orderedOn, price, userId, companyId, status)
    //Offers should contain the customer (business), product, date, address and sign.

    public class Order
    {
        [Key]
        public Guid OrderId {get;set;}
        public Guid ProductId {get;set;}
        public DateTime Date {get;set;}
        public DateTime OrderedOn { get; set; }
        public double Price { get; set; }
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }
     // public string Cilent { get; set; }
        public string Address { get; set; }
        public string Sign { get; set; }
        public string Status { get; set; }

        public Guid CourierId {get;set;}//courier
        public Guid ManagerId {get;set;}
    }
}