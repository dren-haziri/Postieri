using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using Postieri.Data;
using Postieri.DTOs;
using Postieri.Interfaces;

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

        [HttpPost]
        public IActionResult SendEmail()
        {


            string to = "test@gmail.com";

            string subject = "Postieri Order";
            string body = "Postieri order details";

            _emailService.SendEmail(to, subject, body);

            return Ok();
        }
    }
}