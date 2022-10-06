using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Postieri.DTOs
{
    public class CreateMessageDto
    {
        public Guid RecipientConnectionId { get; set; }
        public string Content { get; set; }
    }
}
