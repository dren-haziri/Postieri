using System.ComponentModel.DataAnnotations;

namespace Postieri.DTOs
{
    public class RegisterDto
    {
        
        public string Email { get; set; }
       
        public string Username { get; set; }
        
        public string Password { get; set; }
       // [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; }
        
        public string CompanyName { get; set; }
        
        public string RoleName { get; set; }
        
        public string PhoneNumber { get; set; }
    }
}