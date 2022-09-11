using System.ComponentModel.DataAnnotations;

namespace Postieri.DTOs
{
    public class ResetPasswordDto
    {
     
        public string PasswordResetToken { get; set; }
       
        public string Password { get; set; }
       
        public string ConfirmPassword { get; set; }
    }
}
