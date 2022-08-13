using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Postieri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly ISendGridClient _sendGridClient;
        private readonly IConfiguration _configuration;

        public EmailController(ISendGridClient sendGridClient, IConfiguration configuration)
        {
            _sendGridClient = sendGridClient;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("send-text-mail")]
        public async Task<IActionResult> SendPlainTextEmail(string toEmail, string subject, string body)
        {
            string fromEmail = _configuration.GetSection("SendGrindEmailSettings").GetValue<string>("FromEmail");

            string fromName = _configuration.GetSection("SendGrindEmailSettings").GetValue<string>("FromName");

            var email = new SendGridMessage
            {
                From = new EmailAddress(fromEmail, fromName),
                Subject = subject,
                PlainTextContent = body
            };

            email.AddTo(toEmail);

            var response = await _sendGridClient.SendEmailAsync(email);

            string message = response.IsSuccessStatusCode ? "Email Send" : "Email Sendin Failed";
            return Ok(message);
        }
    }
}
