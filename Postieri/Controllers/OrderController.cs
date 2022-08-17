using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Postieri.Data;
using Postieri.Models;

namespace Postieri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly DataContext _context;
        public OrderController(DataContext context)
        {
            _context = context;

        }
        [HttpGet]
        public async Task<ActionResult<Order>> Get()
        {
            return Ok(await _context.Order.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<Order>>> AddOrder(Order order)
        {

            _context.Order.Add(order);
            await _context.SaveChangesAsync();
            return Ok(await _context.Order.ToListAsync());
        }
     


        [HttpDelete]
        public async Task<ActionResult<List<Order>>> Delete(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
                return BadRequest("order not found");

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return Ok(await _context.Roles.ToListAsync());
        }


    }
}
