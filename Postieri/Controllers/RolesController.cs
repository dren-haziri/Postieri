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
        public ActionResult<List<Role>> Delete(Guid id)
        {
            _rolesService.DeleteRole(id);
            return Ok(_rolesService.GetRoles());
        }
    }
}

