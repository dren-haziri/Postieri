
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
using Postieri.Models;

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
                //RoleName = request.RoleName,
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
                return BadRequest(response);
            }

            return Ok(response);
        }

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
            var response = await _authService.ForgotPassword(request.Email);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

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

        [Authorize(Roles = "Administrator")]
        [HttpPost("Suspened")]
        public async Task<ActionResult<ServiceResponse<string>>> Suspened(string email)
        {
            var response = await _authService.Suspend(email);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("Unsuspened")]
        public async Task<ActionResult<ServiceResponse<string>>> Unsuspened(string email)
        {
            var response = await _authService.Unsuspend(email);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        //[Authorize(Roles = "Administrator")]
        [HttpPut("AssignRole")]
        public async Task<ActionResult<ServiceResponse<string>>> AssignRole(string email, AssignRoleDto request)
        {
            var response = await _authService.AssignRole(email, request.RoleId);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        //[Authorize(Roles = "Administrator")]
        [HttpPut("RevokeRole")]
        public async Task<ActionResult<ServiceResponse<string>>> RevokeRole(string email)
        {
            var response = await _authService.RevokeRole(email);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}