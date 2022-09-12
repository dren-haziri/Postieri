using Postieri.DTOs;

namespace Postieri.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(EmailDto request);
        void SendLastEmail(string lastEmail, string subject, string body);
    }
}