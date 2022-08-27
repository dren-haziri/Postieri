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
        public readonly IRolesService _rolesService;

        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return (ActionResult)await _rolesService.GetAll();
        }

        [HttpPost]
        public async Task<ActionResult<List<Roles>>> AddRole(Roles role)
        {
            return await _rolesService.AddRole(role);
        }

        [HttpPut]
        public async Task<ActionResult<List<Roles>>> UpdateRole(Roles roles)
        {
            return await _rolesService.UpdateRole(roles);
        }

        [HttpDelete]
        public bool Delete(int id)
        {
            return _rolesService.Delete(id);
        }
    }
}

