using Postieri.Services;

namespace Postieri.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string email, string password);
        Task<ServiceResponse<string>> Verify(string verificationtoken);
        Task<ServiceResponse<string>> ForgotPassword(string email);
        Task<ServiceResponse<string>> ResetPassword(string passwordresettoken, string password);
        Task<bool> UserExists(string email);
    }
}
