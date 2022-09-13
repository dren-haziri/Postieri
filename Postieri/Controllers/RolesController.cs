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
        private readonly IRolesService _rolesService;
        public RolesController(DataContext context, IRolesService rolesService)
        {
            _context = context;
            _rolesService = rolesService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Role>>> Get()
        {
            return Ok(await _context.Roles.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<Role>>> AddRole(Role role)
        {
            _rolesService.AddRole(role);
            return Ok(await _context.Roles.ToListAsync());

        }

        [HttpPut]
        public async Task<ActionResult<List<Role>>> UpdateRole(Role request)
        {
            _rolesService.UpdateRole(request);
            return Ok(await _context.Roles.ToListAsync());
           
        }
        [HttpDelete]
        public async Task<ActionResult<List<Role>>> Delete(Guid id)
        {
            _rolesService.DeleteRole(id);
            return Ok(await _context.Roles.ToListAsync());
        }
    }


    }

