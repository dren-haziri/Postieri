using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using Postieri.Models;
using Postieri.Services.EmailService;

namespace Postieri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromForm] Email mailRequest)
        {
            try
            {
                await _emailService.SendEmailAsync(mailRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
