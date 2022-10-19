using Postieri.Data;
using Postieri.Models;

namespace Postieri.Services
{
    public class RolesService : IRolesService
    {
        private readonly DataContext _context;
        public RolesService(DataContext context)
        {
            _context = context;
        }
        public List<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }
        public bool AddRole(Role request)
        {
            var role = new Role()
            {
                RoleId = request.RoleId,
                RoleName = request.RoleName,
                Description = request.Description,
            };
            if (role == null)
            {
                return false;
            }
            else if (RoleExists(role))
            {
                return false;
            }
            else
            {
                _context.Roles.Add(role);
                _context.SaveChanges();
                return true;
            }
        }
        public bool UpdateRole(Role request)
        {
            var role = _context.Roles.Find(request.RoleId);
            var roleName = _context.Roles.Find(request.RoleName);
            if (role == null || roleName == null)
            {
                return false;
            }
            else if (!RoleExists(role))
            {
                return false;
            }
            else
            {
                role.RoleName = request.RoleName;
                role.Description = request.Description;

                _context.SaveChanges();
                return true;
            }
        }
        public async Task<ServiceResponse<string>> DeleteRole(Guid id)
        {
            var user = _context.Users.Where(n => n.RoleId == id).ToList();
            var role = _context.Roles.Find(id);

            if (role == null)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "Role not found."
                };
            }
            if (user.Count != 0)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "Can not delete this role, cause there are users with this role!"
                };

            }
            _context.Roles.Remove(role);
            _context.SaveChanges();
            return new ServiceResponse<string>
            {
                Success = true,
                Message = "Role deleted successfully"
            };
        }
        public bool RoleExists(Role request)
        {
            bool alreadyExist = _context.Roles.Any(x => x.RoleId == request.RoleId || x.RoleName == request.RoleName);
            return alreadyExist;
        }
    }
}
