using System.ComponentModel.DataAnnotations;

namespace Postieri.DTOs
{
    public class RegisterDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, StringLength(100, MinimumLength = 6)]
        public string Username { get; set; }
        [Required, StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string RoleName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}