using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Roles>> AddRole(Roles roles)
        {
            _context.Roles.Add(roles);

            await _context.SaveChangesAsync();

            return await _context.Roles.ToListAsync();
        }

        public bool Delete(int id)
        {
            var roles = _context.Roles.Find(id);

            if (roles is null)
                return false;

            _context.Roles.Remove(roles);

            _context.SaveChangesAsync();

            return true;
        }

        public async Task<IList<Roles>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<List<Roles>> UpdateRole(Roles roles)
        {
            var role = await _context.Roles.FindAsync(roles.Id);

            role.Name = roles.Name;

            role.Description = roles.Description;

            await _context.SaveChangesAsync();

            return await _context.Roles.ToListAsync();
        }
    }
}
