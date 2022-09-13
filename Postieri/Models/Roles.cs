using System.ComponentModel.DataAnnotations;

namespace Postieri.Models
{
    public class Roles
    {
        [Key]
        public Guid Id{ get; set; }
        public string Name { get; set; } 
        public string Description { get; set; }  
    }
}
