using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Postieri.Controllers
{
    public class AdministrationController : Controller
    {
        //private readonly RoleManager<IdentityRole> roleManager;
        //public AdministrationController(RoleManager<IdentityRole> roleManager)
     //   {
          //  this.roleManager = roleManager; 
     //   }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        //[HttpPost]
        //public IActionResult CreateRole()
        //{
        //    return View();
        //}
    }
}
