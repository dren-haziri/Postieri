using System.ComponentModel.DataAnnotations;

namespace Postieri.DTOs
{
    public class ResetPasswordDto
    {
        [Required]
        public string PasswordResetToken { get; set; } = string.Empty;
        [Required, StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
