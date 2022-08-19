using System.ComponentModel.DataAnnotations;

namespace Postieri.Models
{
    public class UserResetPassword
    {
        [Required]
        public string PasswordResetToken { get; set; } = string.Empty;
        [Required, StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
