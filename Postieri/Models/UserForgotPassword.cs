using System.ComponentModel.DataAnnotations;

namespace Postieri.Models
{
    public class UserForgotPassword
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
