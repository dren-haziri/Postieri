namespace Postieri.Models
{
    public class Message
    {
        public int Id { get; set; }
        public Guid From { get; set; }
        public Guid To { get; set; }
        public string Content { get; set; }
    }
}
