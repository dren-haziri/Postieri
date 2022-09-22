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
                Name = request.Name,
                Description = request.Description,
            };
            if(role == null)
            {
                return false;
            }
            else if(RoleExists(role))
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
            if (role == null)
            {
                return false;
            }
            else if (!RoleExists(role))
            {
                return false;
            }
            else
            {
                role.Name = request.Name;
                role.Description = request.Description;

                _context.SaveChanges();
                return true;
            }
        }
        public bool DeleteRole(Guid id)
        {
            var role = _context.Roles.Find(id);
            if (role == null)
            {
                return false;
            }
            else if (!RoleExists(role))
            {
                return false;
            }
            else
            {
                _context.Roles.Remove(role);
                _context.SaveChanges();
                return true;
            } 
        }
        public bool RoleExists(Role request)
        {
            bool alreadyExist = _context.Roles.Any(x => x.RoleId == request.RoleId || x.Name == request.Name || x.Description == request.Description);
            return alreadyExist;
        }
    }
}
