using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Postieri
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string RoleName { get; set; }
        public string Status { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }
        public DateTime ExpDate { get; set; } = DateTime.Now.AddYears(1);
        public bool IsLocked { get; set; }
    }
}