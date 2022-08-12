using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Postieri.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }

        public string?Name {get;set;}
         public double Price { get; set; }
         public double ProductSize {get;set;}
         public double productWeight {get;set;}
    }
}