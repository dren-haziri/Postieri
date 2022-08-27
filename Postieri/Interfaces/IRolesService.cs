using Postieri.Models;
using Postieri.Services;

namespace Postieri.Interfaces
{
    public interface IRolesService
    {
        Task<IList<Roles>> GetAll();
        Task<List<Roles>> AddRole(Roles roles);
        Task<List<Roles>> UpdateRole(Roles roles);
        bool Delete(int id);
    }
}
