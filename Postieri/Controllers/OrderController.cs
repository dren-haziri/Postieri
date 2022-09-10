﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Postieri.Data;
using Postieri.Models;
using Postieri.Services;

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
        [HttpGet("{id}")]
        public  async Task<ActionResult<Order>> GetOrderById(Guid orderId)
        {
            var errorResponse = new ServiceResponse<string>();


            if (_context.Orders == null)
            {
                errorResponse.Success = false;
                errorResponse.Message = "Order not found.";
                return BadRequest(errorResponse);

            }     
            var _order = _context.Orders.FirstOrDefault(n => n.OrderId == orderId);
            if (_order == null)
            {
                errorResponse.Success = false;
                errorResponse.Message = "Order doesn't exists!";
                return BadRequest(errorResponse);
            }
            
            //var response = new ServiceResponse<Order>();
            //response.Data = _order;
            //response.Success = true;
            //response.Message = "Here's your order ";


            return Ok(_order)  ;
        }
        [HttpPost]
        public async Task<ActionResult<List<Order>>> AddOrder(Order orders)
        {

            _context.Orders.Add(orders);
            await _context.SaveChangesAsync();
            return Ok(await _context.Orders.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<Order>>> UpdateOrder(Order request)
        {
           
            var orders = await _context.Orders.FindAsync(request.OrderId);
            if (orders == null)
                return BadRequest("Order not found");
            orders.Price = request.Price;
            orders.Address = request.Address;
            orders.Sign = request.Sign;
            orders.Status = request.Status;
            orders.CourierId = request.CourierId;

            await _context.SaveChangesAsync();
            return Ok(await _context.Orders.ToListAsync());
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
