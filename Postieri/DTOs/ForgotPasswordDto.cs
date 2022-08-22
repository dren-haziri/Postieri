using System.ComponentModel.DataAnnotations;

namespace Postieri.DTOs
{
    public class ForgotPasswordDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
