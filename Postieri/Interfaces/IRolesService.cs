using Postieri.Models;
using Postieri.Services;

namespace Postieri.Interfaces
{
    public interface IRolesService
    {
        List<Role> GetRoles();
        bool AddRole(Role request);
        bool UpdateRole(Role request);
        Task<ServiceResponse<string>> DeleteRole(Guid id);
    }
}
