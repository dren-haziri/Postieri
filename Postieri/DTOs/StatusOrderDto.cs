namespace Postieri.DTOs
{
    public class StatusOrderDto
    {
        public Guid OrderId { get; set; }
        public string Status { get; set; }
        public Guid? CourierId { get; set; }
    }
}
