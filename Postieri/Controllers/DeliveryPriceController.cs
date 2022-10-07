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
        public async Task<ActionResult<List<DeliveryPrice>>> Get()
        {
            return Ok(await _deliveryPriceService.Get());
        }

        [HttpPost]
        public async Task<ActionResult<List<DeliveryPrice>>> AddCalculatePrice(DeliveryPrice request)
        {
            _deliveryPriceService.AddCalculatePrice(request);
            return Ok(await _deliveryPriceService.Get());
        }

        [HttpPut]
        public async Task<ActionResult<List<DeliveryPrice>>> UpdateCalculatePrice(DeliveryPrice request)
        {
            _deliveryPriceService.UpdateCalculatePrice(request);
            return Ok(await _deliveryPriceService.Get());
        }

        [HttpDelete]
        public async Task<ActionResult<List<DeliveryPrice>>> Delete(Guid DeliveryPriceId)
        {
            _deliveryPriceService.Delete(DeliveryPriceId);
            return Ok(await _deliveryPriceService.Get());
        }
    }
}
