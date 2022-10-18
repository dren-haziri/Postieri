using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Postieri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculationController : ControllerBase
    {
        private readonly ICalculationService _calculationService;
        public CalculationController(ICalculationService calculationService)
        {
            _calculationService = calculationService;
        }
        [HttpGet("CalculateSize")]
        public string CalculateSize(double length, double width, double height)
        {
            return _calculationService.CalculateSize(length, width, height); 
         
        }
    }
}
