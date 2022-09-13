using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Postieri.Data;

namespace Postieri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public string CalculateSize(double length, double width, double height)
        {
            //Small Package Width
            var SPWidth = from d in _context.Dimensions
                          where d.name == "SmallPackage"
                          select d.width;
            var SmallPackageWidth = SPWidth.FirstOrDefault();
            //Small Package Height
            var SPHeight = from d in _context.Dimensions
                           where d.name == "SmallPackage"
                           select d.height;
            var SmallPackageHeight = SPHeight.FirstOrDefault();
            //Small Package width
            var SPLength = from d in _context.Dimensions
                           where d.name == "SmallPackage"
                           select d.length;
            var SmallPackageLength = SPLength.FirstOrDefault();
            //Medium height
            var MPHeight = from d in _context.Dimensions
                           where d.name == "MediumPackage"
                           select d.height;
            var MediumPackageHeight = MPHeight.FirstOrDefault();

            //medium width
            var MPWidth = from d in _context.Dimensions
                          where d.name == "MediumPackage"
                          select d.width;
            var MediumPackageWidth = MPWidth.FirstOrDefault();
            // medium length
            var MPLength = from d in _context.Dimensions
                           where d.name == "MediumPackage"
                           select d.length;
            var MediumPackageLength = MPLength.FirstOrDefault();
            //LargePackageLength
            var LPLength = from d in _context.Dimensions
                           where d.name == "LargePackage"
                           select d.length;
            var LargePackageLength = LPLength.FirstOrDefault();
            //LargePackageLength
            var LPwidth = from d in _context.Dimensions
                          where d.name == "LargePackage"
                          select d.width;
            var LargePackageWidth = LPwidth.FirstOrDefault();
            //LargePackageHeight
            var LPHeight = from d in _context.Dimensions
                           where d.name == "LargePackage"
                           select d.height;
            var LargePackageHeight = LPHeight.FirstOrDefault();


            if (height < SmallPackageHeight && width < SmallPackageWidth && length < SmallPackageLength)
            {
                return ("this is a small package");
            }

            else if (height > SmallPackageHeight && height < MediumPackageHeight && width > SmallPackageWidth && width < MediumPackageWidth && length > SmallPackageLength && length < MediumPackageLength)
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
