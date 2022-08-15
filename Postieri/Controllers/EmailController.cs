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
        private readonly IConfiguration _getValues;

        public EmailController(ISendGridClient sendGridClient, IConfiguration getValues)
        {
            _sendGridClient = sendGridClient;
            _getValues = getValues;
        }

        [HttpGet]
        [Route("send-text-mail")]
        public async Task<IActionResult> SendTextEmail(string toEmail, string subject, string body)
        {
            string fromEmail = _getValues.GetSection("SendGrindEmailSettings").GetValue<string>("FromEmail");

            string fromName = _getValues.GetSection("SendGrindEmailSettings").GetValue<string>("FromName");

            var email = new SendGridMessage
            {
                From = new EmailAddress(fromEmail, fromName),
                Subject = subject,
                PlainTextContent = body
            };

            email.AddTo(toEmail);

            var response = await _sendGridClient.SendEmailAsync(email);

            string message = response.IsSuccessStatusCode ? "Email Send" : "Email Sending Failed";
            return Ok(message);
        }
    }
}
