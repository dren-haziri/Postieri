using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Postieri.DTOs
{
    public class MessageDto
    {
        public int Id { get; set; }
        public Guid? SenderId { get; set; }
        public Guid SenderConnectionId { get; set; }
        public Guid? RecipientId { get; set; }
        public Guid RecipientConnectionId { get; set; }
        public string Content { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime MessageSent { get; set; } = DateTime.UtcNow;
    }
}
