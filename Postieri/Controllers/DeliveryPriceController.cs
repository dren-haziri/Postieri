using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Postieri.Data;
using Postieri.Models;

namespace Postieri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryPriceController : ControllerBase
    {
        private readonly IDeliveryPriceService _deliveryPriceService;
        public DeliveryPriceController(IDeliveryPriceService deliveryPriceService)
        {
            _deliveryPriceService = deliveryPriceService;
        }
        [HttpGet]
        public ActionResult<List<DeliveryPrice>> Get()
        {
            return Ok(_deliveryPriceService.GetCalculations());
        }

        [HttpPost]
        public ActionResult<List<DeliveryPrice>> AddCalculatePrice(DeliveryPrice request)
        {
            _deliveryPriceService.AddCalculation(request);
            return Ok(_deliveryPriceService.GetCalculations());
        }

        [HttpDelete]
        public ActionResult<List<DeliveryPrice>> Delete(Guid DeliveryPriceId)
        {
            _deliveryPriceService.DeleteCalculation(DeliveryPriceId);
            return Ok(_deliveryPriceService.GetCalculations());
        }
    }
}
