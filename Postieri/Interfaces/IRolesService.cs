using Postieri.Models;

namespace Postieri.Interfaces
{
    public interface IRolesService
    {
        List<Role> GetRoles();
        bool AddRole(Role request);
        bool UpdateRole(Role request);
        bool DeleteRole(Guid id);
    }
}
