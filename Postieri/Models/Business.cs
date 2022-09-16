using System.ComponentModel.DataAnnotations;

namespace Postieri.Models
{
    public class Business
    {
        [Key]
        public int BusinessID { get; set; }
        public string  BusinessName { get; set; }
        public string BusinessToken { get; set; }
    }
}
