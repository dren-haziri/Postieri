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
        public ActionResult<List<Role>> AddRole(Role role)
        {
            return Ok( _rolesService.AddRole(role));
        }

        [HttpPut]
        public ActionResult<List<Role>> UpdateRole(Role request)
        {
            return Ok(_rolesService.UpdateRole(request));
        }

        [HttpDelete]
        public ActionResult<List<Role>> Delete(Guid id)
        {
            return Ok(_rolesService.DeleteRole(id));
        }
    }
}

