using Postieri.Models;

namespace Postieri.Interfaces
{
    public interface IRolesService
    {
        void AddRole(Role role);
        public void UpdateRole(Role request);
        public void DeleteRole(Guid id);
    }
}
