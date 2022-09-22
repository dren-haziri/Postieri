using System.ComponentModel.DataAnnotations;

namespace Postieri.Models
{
    public class Role
    {
        [Key]
        public Guid RoleId{ get; set; }
        public string Name { get; set; }  
        public string Description { get; set; }  
    }
}
