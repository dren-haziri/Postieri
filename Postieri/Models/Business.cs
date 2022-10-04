using System.ComponentModel.DataAnnotations;

namespace Postieri.Models
{
    public class Business
    {
        [Key]
        public int BusinessID { get; set; }
        public string  BusinessName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber  { get; set; }
        public string BusinessToken { get; set; }
    }
}
