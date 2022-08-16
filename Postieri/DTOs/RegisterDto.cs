using System.ComponentModel.DataAnnotations;

namespace Postieri
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string RoleName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
