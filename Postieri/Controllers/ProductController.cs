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
            var SmallPackageLength = 353;
            var SmallPackageWidth = 250;
            var SmallPackageHeight = 20.5;   
            var MediumPackageLength = 450;
            var MediumPackageWidth = 350;
            var MediumPackageHeight = 160;
            var LargePackageLength = 610;
            var LargePackageWidth = 460;
            var LargePackageHeight = 460;

            if (height < SmallPackageHeight && width < SmallPackageWidth && length <SmallPackageLength )
            {
                return ("this is a small package");
            }
            else if (height>SmallPackageHeight && height<MediumPackageHeight && width > SmallPackageWidth && width < MediumPackageWidth && length>SmallPackageLength && length<MediumPackageLength)
            {
                return ("this is a medium package");
            }
            else if (length > MediumPackageLength && length < LargePackageLength && width > MediumPackageWidth && width < LargePackageWidth && height > MediumPackageHeight && height < LargePackageHeight)
            {
                return ("this is a large package");
            }
                return ("we do not ship this kind of package, please contact our staff for further details");
        }
    }
}
