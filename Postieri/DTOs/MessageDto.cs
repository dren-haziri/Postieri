using System;

namespace Postieri.DTOs
{
    public class MessageDto
    {
        public int Id { get; set; } //mesazhi ka nje id
        public int SenderId { get; set; }
        public string SenderUsername { get; set; }
        public int RecipientId { get; set; }
        public string RecipientUsername { get; set; }
        public string Content { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime MessageSent { get; set; }
  
    }
}
