using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Postieri;
using Postieri.Data;

namespace Postieri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, UserDto user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            var _user = _context.Users.FirstOrDefault(n => n.UserId == id);
            if (_user != null)
            {
               
                _user.PhoneNumber = user.PhoneNumber;
                _user.Email = user.Email;
                _user.IsSuspended = user.IsSuspended;
                _user.Status = user.IsSuspended ?  "Suspended" : "Active" ;
                _user.Username = user.Username;
                _user.CompanyName = user.CompanyName;           
                _user.RoleId = user.RoleId;
                var role = _context.Roles.FirstOrDefault(n => n.RoleId == user.RoleId).RoleName;
                _user.RoleName = role;
               
        

                _context.SaveChanges();
            }

            return NoContent();
        }


        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }



    }
}
