using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Postieri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    { 
        //small package(l:353mm, w:250mm,h:20.5mm)
        //medium package(l:450mm, w:350, h:160mm)
        //large package(l:610, w:460mm, h:460mm)

        public string CalculateSize(double length, double width, double height)
        {
            if (height < 353 && width < 250 && height < 20.5)
            {
                return ("this is a small package");
            }
            else if (height > 353 && height < 450 && width > 250 && width < 350 && height > 20.5 && height < 160)
            {
                return ("this is a medium package");
            }
            else if (height > 450 && height < 610 && width > 350 && width < 460 && height > 160 && height < 460)
            {
                return ("this is a large package");
            }
            else
            {
                return ("we do not ship this kind of package, please contact our staff for further details");
            }

        }
    }
}
