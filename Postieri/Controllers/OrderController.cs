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
            return Ok(await _context.Orders.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<Order>>> AddOrder(Order orders)
        {

            _context.Orders.Add(orders);
            await _context.SaveChangesAsync();
            return Ok(await _context.Orders.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<Order>>> UpdateRole(Order request)
        {
           
            var orders = await _context.Orders.FindAsync(request.OrderId);
            if (orders == null)
                return BadRequest("role not found");
            orders.Price = request.Price;
            orders.Address = request.Address;
            orders.Sign = request.Sign;
            orders.Status = request.Status;
            orders.CourierId = request.CourierId;

            await _context.SaveChangesAsync();
            return Ok(await _context.Roles.ToListAsync());
        }



            [HttpDelete]
        public async Task<ActionResult<List<Order>>> Delete(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return BadRequest("order not found");

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return Ok(await _context.Roles.ToListAsync());
        }


    }
}
