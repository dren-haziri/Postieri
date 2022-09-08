using System.ComponentModel.DataAnnotations;

namespace Postieri.DTOs
{
    public class ResetPasswordDto
    {
        [Required]
        public string PasswordResetToken { get; set; }
        [Required, StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
