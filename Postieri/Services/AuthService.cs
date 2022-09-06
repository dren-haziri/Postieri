﻿using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using Postieri.Data;
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

            return new ServiceResponse<int> { Data = user.UserId, Message = "Registration successful!" }.ToJson();
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
            else
            {
                response.Data = user.VerificationToken;
                response.Message = "Login successful!";
            }

            return response.ToJson();
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
                response.Message = "Verification successful!";

                user.VerifiedAt = DateTime.Now;
            }
            await _context.SaveChangesAsync();

            return response.ToJson();
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

            return response.ToJson();
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

            return response.ToJson();
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

        public async Task<ServiceResponse<bool>> ChangePassword(int userId, string newPassword)
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

            return new ServiceResponse<bool> { Data = true, Message = "Password has been changed." }.ToJson();
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
                Success = true,
                Message = "User was suspended"
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
                Success = true,
                Message = "User was unsuspended"
            };
        }

    }
}
