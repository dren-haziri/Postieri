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
        public List<Role> AddRole(Role role)
        {
            var _role = new Role()
            {
                Name = role.Name,
                Description = role.Description,
            };
            _context.Roles.Add(_role);
            _context.SaveChanges();
            return _context.Roles.ToList();
        }
        public List<Role> UpdateRole(Role request)
        {
            var role = _context.Roles.Find(request.Id);
            role.Name = request.Name;
            role.Description = request.Description;

            _context.SaveChanges();
            return _context.Roles.ToList();
        }
        public List<Role> DeleteRole(Guid id)
        {
            var role = _context.Roles.Find(id);

            _context.Roles.Remove(role);
            _context.SaveChanges();
            return _context.Roles.ToList();
        }
    }
}
