using Postieri.Models;

namespace Postieri.Services.EmailService
{
    public interface IEmailService
    {
        Task SendEmailAsync(Email mailRequest);
    }
}