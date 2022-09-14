using Postieri.DTOs;

namespace Postieri.Interfaces
{
    public interface IEmailService
    { 
        void SendEmail(string to, string subject, string body);
    }
}