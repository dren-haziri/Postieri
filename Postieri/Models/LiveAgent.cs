using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Postieri.Models
{
    public class LiveAgent : User
    {
        public Guid? ConnectionId { get; set; }
    }
}
