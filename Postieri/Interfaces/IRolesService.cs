using Postieri.Models;

namespace Postieri.Interfaces
{
    public interface IRolesService
    {
        List<Role> GetRoles();
        List<Role> AddRole(Role role);
        List<Role> UpdateRole(Role request);
        List<Role> DeleteRole(Guid id);
    }
}
