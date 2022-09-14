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
                Name = request.Name,
                Description = request.Description,
            };
            if(role == null && RoleExists(role))
                return false;
            _context.Roles.Add(role);
            _context.SaveChanges();
            return true;
        }
        public bool UpdateRole(Role request)
        {
            var role = _context.Roles.Find(request.Id);
            if (role == null && RoleExists(role))
                return false;
            role.Name = request.Name;
            role.Description = request.Description;

            _context.SaveChanges();
            return true;
        }
        public bool DeleteRole(Guid id)
        {
            var role = _context.Roles.Find(id);
            if (role == null && RoleExists(role))
                return false;
            _context.Roles.Remove(role);
            _context.SaveChanges();
            return true;
        }
        public bool RoleExists(Role request)
        {
            var roleList = _context.Roles.ToList();
            if (roleList.Contains(request))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
