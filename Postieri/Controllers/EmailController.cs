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
        private readonly DataContext _context;
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService, DataContext context)
        {
            _emailService = emailService;
            _context = context;
        }

        [HttpPost]
        public IActionResult SendEmail()
        {
            //var getLastEmail = (from d in _context.Users
            //                    orderby d ascending
            //                    select d.Email
            //  ).LastOrDefault();

            string getLastEmail = "test@gmail.com";

            string subject = "Postieri Order";
            string body = "Postieri order details";

            _emailService.SendLastEmail(getLastEmail, subject, body);

            return Ok();
        }
    }
}