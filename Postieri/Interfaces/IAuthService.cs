using Postieri.Models;
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
        Task<ServiceResponse<string>> Suspend(string email);
        Task<ServiceResponse<string>> Unsuspend(string email);
        Task<ServiceResponse<string>> AssignRole(string email, Guid roleId);
        Task<ServiceResponse<string>> RevokeRole(string email);
        string GetMyName();
        List<Order> GetOrders();
    }
}
