using Postieri.Data;
using Postieri.Models;
using Microsoft.EntityFrameworkCore;

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
            return _context.Roles.Include(x => x.Users).ToList();
        }
        public bool AddRole(Role request)
        {
            var role = new Role()
            {
                RoleId = request.RoleId,
                RoleName = request.RoleName,
                Description = request.Description,
            };
            if (RoleExistsOrNull(role))
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
            
            if (!RoleExistsOrNull(role))
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
        public bool RoleExistsOrNull(Role request)
        {
            if (request == null)
            { 
                return true; 
            }
            return _context.Roles.Any(x => x.RoleId == request.RoleId || x.RoleName == request.RoleName);
        }
    }
}