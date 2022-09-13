using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Postieri.Data;
using Postieri.Models;

namespace Postieri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {

      
        private readonly DataContext _context;
        public RolesController(DataContext context)
        {
            _context = context;

        }
        [HttpGet]
        public async Task<ActionResult<List<Role>>> Get()
        {
            return Ok(await _context.Roles.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<Role>>> AddRole(Role role)
        {
            //allRoles.Add(role);
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return Ok(await _context.Roles.ToListAsync());

        }

        [HttpPut]
        public async Task<ActionResult<List<Role>>> UpdateRole(Role request)
        {
            //var role = allRoles.Find(r => r.Id == request.Id);
            var role =await  _context.Roles.FindAsync(request.Id);
            if (role == null)
                return BadRequest("role not found");
            role.Name = request.Name;
            role.Description = request.Description;

            await _context.SaveChangesAsync();
            return Ok(await _context.Roles.ToListAsync());
           
        }
        [HttpDelete]
        public async Task<ActionResult<List<Role>>> Delete(Guid id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
                return BadRequest("role not found");

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return Ok(await _context.Roles.ToListAsync());
        }
    }


    }

