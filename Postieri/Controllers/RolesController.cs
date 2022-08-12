using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Postieri.Models;

namespace Postieri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {

        public static List<Roles> allRoles = new List<Roles>
        {
            {
                new Roles
                {
                    Id = 1,
                    Name ="Admin"
                }

            }
        };
        [HttpGet]
        public async Task<ActionResult<List<Roles>>> Get()
        {
            return Ok(allRoles);
        }

        [HttpPost]
        public async Task<ActionResult<List<Roles>>> AddRole(Roles role)
        {
            allRoles.Add(role);
            return Ok(allRoles);

        }

        [HttpPut]
        public async Task<ActionResult<List<Roles>>> UpdateRole(Roles request)
        {
            var role = allRoles.Find(r => r.Id == request.Id);
            if (role == null)
                return BadRequest("role not found");
                role.Name = request.Name;

                return Ok(allRoles);
           
        }
        [HttpDelete]
        public async Task<ActionResult<List<Roles>>> Delete(int id)
        {
            var role = allRoles.Find(r => r.Id == id);
            if (role == null)
                return BadRequest("role not found");

            allRoles.Remove(role);
            return Ok(allRoles);
        }
    }


    }

