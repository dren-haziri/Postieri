using System;

namespace Postieri.Models
{
    public class Message
    {
        public int Id { get; set; }
        public Guid? SenderId { get; set; }
        public Guid SenderConnectionId { get; set; }
        public LiveAgent? Sender { get; set; }
        public Guid? RecipientId { get; set; }
        public Guid RecipientConnectionId { get; set; }
        public LiveAgent? Recipient { get; set; }
        public string Content { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime MessageSent { get; set; } = DateTime.UtcNow;
    }
}
