using Postieri.DTOs;

namespace Postieri.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(EmailDto request);
    }
}
