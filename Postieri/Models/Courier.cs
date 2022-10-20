using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Postieri.Models
{
    public class Courier : User
    {
        public bool IsAvailable { get; set; } = true;
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }

        public List<Order>? Orders { get; set; }    

    }
}
