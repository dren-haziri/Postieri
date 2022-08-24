using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Postieri.Services;
using Microsoft.AspNetCore.Authorization;
using System.Configuration;
using Postieri.Interfaces;

namespace Postieri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(RegisterDto request)
        {
            var user = new User
            {
                Email = request.Email,
                Username = request.Username,
                CompanyName = request.CompanyName,
                RoleName = request.RoleName,
                PhoneNumber = request.PhoneNumber,
            };

            var response = await _authService.Register(user, request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(LoginDto request)

        {
            var response = await _authService.Login(request.Email, request.Password);
            if (!response.Success)
            {

                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.RoleName)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                return BadRequest(response);
            }


            return Ok(response);
        }


            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        [HttpPost("verify")]
        public async Task<ActionResult<ServiceResponse<string>>> Verify(UserVerificationDto request)
        {
            var response = await _authService.Verify(request.VerificationToken);
            if (!response.Success)
            {
                return BadRequest(response);
            }


            return Ok(response);
        }

        [HttpPost("forgot-password")]
        public async Task<ActionResult<ServiceResponse<string>>> ForgotPassword(ForgotPasswordDto request)
        {

            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            var response = await _authService.ForgotPassword(request.Email);
            if (!response.Success)
            {
                return BadRequest(response);

            }

            return Ok(response);
        }


        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        //private bool LockUser(string email)
        //{
        //    email = "string";
        //    user.Email = email;
        //    user.IsLocked = true;
        //    if (user.IsLocked)
        //    {
        //        return true ;
        //    }
        //    return false;

        //}
        [HttpPost("LockUser")]
        public void LockUser(string email)
        {
            user.IsLocked = true;
        }


        [HttpPost("UnLockUser")]
        public void UnLockUser(string email)
        {
            user.IsLocked = false;
        }

        private bool isUserLocked(string email)
        {
            if (user.IsLocked)
            {
                return true;
            }
            return false;

        [HttpPost("reset-password")]
        public async Task<ActionResult<ServiceResponse<string>>> ResetPassword(ResetPasswordDto request)
        {
            var response = await _authService.ResetPassword(request.PasswordResetToken, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);

        }

    }
}