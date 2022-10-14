using Postieri.Models;

namespace Postieri.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(Email request);
    }
}
