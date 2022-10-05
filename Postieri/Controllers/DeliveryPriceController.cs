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
        private readonly DataContext _context;
        public DeliveryPriceController(DataContext context)
        {
            _context = context;

        }
        [HttpGet]
        public async Task<ActionResult<List<DeliveryPrice>>> Get()
        {
            return Ok(await _context.DeliveryPrices.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<DeliveryPrice>>> AddCalculatePrice(DeliveryPrice request)
        {
            _context.DeliveryPrices.Add(request);
            await _context.SaveChangesAsync();
            return Ok(await _context.DeliveryPrices.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<DeliveryPrice>>> UpdateCalculatePrice(DeliveryPrice request)
        {
            var deliceryPrice = await _context.DeliveryPrices.FindAsync(request.DeliveryPriceId);
            if (deliceryPrice == null)
                return BadRequest("Calculated price not found");
            deliceryPrice.CountryTo = request.CountryTo;
            deliceryPrice.CityTo = request.CityTo;
            deliceryPrice.PostCodeTo = request.PostCodeTo;
            deliceryPrice.Dimension = request.Dimension;
            deliceryPrice.TotalPrice = request.TotalPrice;

            await _context.SaveChangesAsync();
            return Ok(await _context.DeliveryPrices.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<DeliveryPrice>>> Delete(Guid DeliveryPriceId)
        {
            var deliceryPrice = await _context.DeliveryPrices.FindAsync(DeliveryPriceId);
            if (deliceryPrice == null)
                return BadRequest("Calculated price not found");

            _context.DeliveryPrices.Remove(deliceryPrice);
            await _context.SaveChangesAsync();
            return Ok(await _context.DeliveryPrices.ToListAsync());
        }
    }
}
