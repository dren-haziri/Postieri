using Postieri.Models;
using Postieri.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Postieri
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
     
        public string Email { get; set; }
        public string CompanyName { get; set; }
        [ForeignKey("RoleId")]
        public Role? Role { get; set; }
        public Guid? RoleId { get; set; }
        public string RoleName { get; set; } = "NoRole";
        public bool IsSuspended { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public DateTime ExpirationDate { get; set; } = DateTime.Now.AddYears(1);
        //User forget reset password
        public string? VerificationToken { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public string? Status { get; set; }
      

    }
}