using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using Postieri.Data;
using Postieri.Models;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Postieri.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            if (await UserExists(user.Email))
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "User already exists."
                };
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.VerificationToken = CreateToken(user);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new ServiceResponse<int> { IDs = user.UserId, Message = "Registration successful!" };
        }

        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));

            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password.";
            }
            else if (user.VerifiedAt == null)
            {
                response.Success = false;
                response.Message = "User is not verified.";
            }
            else if (user.IsSuspended)
            {
                response.Success = false;
                response.Message = "User is suspended.";
            }
            else if (user.RoleName == "NoRole")
            {
                response.Success = false;
                response.Message = "User needs to be assigned a role.";
            }
            else
            {
                response.IDs = user.UserId;
                response.Data = "Welcome " + user.Username;
                response.Message = "Login successful!";
            }

            return response;
        }

        public async Task<ServiceResponse<string>> Verify(string verificationtoken)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.VerificationToken.Equals(verificationtoken));
            if (user == null)
            {
                response.Success = false;
                response.Message = "Invalid token.";
            }
            else
            {
                response.IDs = user.UserId;
                response.Data = user.VerificationToken;
                response.Message = "Verification successful!";

                user.VerifiedAt = DateTime.Now;
            }
            await _context.SaveChangesAsync();

            return response;
        }

        public async Task<ServiceResponse<string>> ForgotPassword(string email)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
            }
            else
            {
                response.Message = "You may now reset your password.";

                user.PasswordResetToken = CreatePasswordResetToken();
                user.ResetTokenExpires = DateTime.Now.AddDays(1);
            }
            await _context.SaveChangesAsync();

            return response;
        }

        public async Task<ServiceResponse<string>> ResetPassword(string passwordresettoken, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.PasswordResetToken.Equals(passwordresettoken));
            if (user == null || user.ResetTokenExpires < DateTime.Now)
            {
                response.Success = false;
                response.Message = "Invalid Token.";
            }
            else
            {
                response.Message = "Password successfully reset.";

                CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.PasswordResetToken = null;
                user.ResetTokenExpires = null;
            }

            await _context.SaveChangesAsync();

            return response;
        }

        private string CreatePasswordResetToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

        public async Task<bool> UserExists(string email)
        {
            
                return await _context.Users.AnyAsync(user => user.Email.ToLower()
                 .Equals(email.ToLower()));
            
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash =
                    hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.RoleName)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<ServiceResponse<bool>> ChangePassword(Guid userId, string newPassword)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true, Message = "Password has been changed." };
        }

        public async Task<ServiceResponse<string>> Suspend(string email)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
            if (user == null)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "User not found."
                };
            }
            user.IsSuspended = true;
            await _context.SaveChangesAsync();
            return new ServiceResponse<string>
            {
                IDs = user.UserId,
                Success = true,
                Message = "User has been suspended"
            };
        }

        public async Task<ServiceResponse<string>> Unsuspend(string email)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
            if (user == null)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "User not found."
                };
            }
            user.IsSuspended = false;
            await _context.SaveChangesAsync();
            return new ServiceResponse<string>
            {
                IDs = user.UserId,
                Success = true,
                Message = "User has been unsuspended"
            };
        }

        public async Task<ServiceResponse<string>> AssignRole(string email, Guid roleId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
            var role = _context.Roles.Find(roleId);

            if (user == null)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "User not found."
                };
            }
            if (role == null)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "Role does not exist."
                };
            }

            user.RoleId = roleId;
            user.RoleName = role.RoleName;
            user.VerificationToken = CreateToken(user); //new jwt needed bc the ClaimTypes.Role has to change regarding the new Role assignment

            if (role.RoleName == "Courier")
            {

                Courier courier = new Courier()
                {
                    UserId = user.UserId,
                    CompanyName = user.CompanyName,
                    RoleName = user.RoleName,
                    PasswordSalt = user.PasswordSalt,
                    Username = user.Username,
                    Email = user.Email,
                    VehicleId = 1,
                    PasswordHash = user.PasswordHash,
                    PhoneNumber = user.PhoneNumber,
                    RoleId = user.RoleId,
                    RegisterDate = user.RegisterDate,
                    IsActive = user.IsActive,
                    ExpirationDate = user.ExpirationDate,
                    VerifiedAt = user.VerifiedAt,
                    IsSuspended = user.IsSuspended,
                    VerificationToken = user.VerificationToken,
                    PasswordResetToken = user.PasswordResetToken,
                    ResetTokenExpires = user.ResetTokenExpires,
                };

                _context.Users.Remove(user);
                _context.Couriers.Add(courier);
            }
                await _context.SaveChangesAsync();

            return new ServiceResponse<string> 
            { 
                IDs = roleId, 
                Data = user.Username + " has been assigned as a " + user.RoleName, 
                Success = true, 
                Message = "Role updated successfully" 
            };
        }

        public async Task<ServiceResponse<string>> RevokeRole(string email)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
            if (user == null)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            user.RoleId = null;
            user.RoleName = "NoRole";
            await _context.SaveChangesAsync();

            return new ServiceResponse<string> 
            { 
                IDs = user.UserId,
                Data = user.Username + " has been revoked of their role",
                Success = true, 
                Message = "Role revoked successfully" 
            };
        }
    }
}
