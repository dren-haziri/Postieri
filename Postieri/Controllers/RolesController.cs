using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Postieri.Data;
using Postieri.Models;
using Postieri.Services;

namespace Postieri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesService;
        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpGet]
        public ActionResult<List<Role>> Get()
        {
            return Ok( _rolesService.GetRoles());
        }

        [HttpPost]
        public ActionResult<List<Role>> AddRole(Role request)
        {
            _rolesService.AddRole(request);
            return Ok(_rolesService.GetRoles());
        }

        [HttpPut]
        public ActionResult<List<Role>> UpdateRole(Role request)
        {
            _rolesService.UpdateRole(request);
            return Ok(_rolesService.GetRoles());
        }

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<string>>> DeleteRole(Guid id)
        {
            var response = await _rolesService.DeleteRole(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}

