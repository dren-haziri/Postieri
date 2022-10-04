namespace Postieri.DTO
{
    public class ClientOrderDto
    {
        public Guid ProductId { get; set; }
        public DateTime Date { get; set; }
        public DateTime OrderedOn { get; set; }
        public double Price { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        public string JWT { get; set; }
    }
}
