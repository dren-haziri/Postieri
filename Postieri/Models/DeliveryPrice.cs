using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Postieri.Models
{
    public class DeliveryPrice
    {
        [Key]
        public Guid DeliveryPriceId { get; set; }
        public string CountryTo { get; set; }
        public string CityTo { get; set; }
        public int PostCodeTo { get; set; }
        public Dimension? Dimension { get; set; }
        public double TotalPrice { get; set; }
    }
}
