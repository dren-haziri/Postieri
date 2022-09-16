using Microsoft.AspNetCore.Http;
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
        private readonly IOrderService _orderService;
        private readonly DataContext _context;
        public OrderController(IOrderService orderService, DataContext context)
        {
            _orderService = orderService;
            _context = context;
        }
        [HttpGet]
        public ActionResult<List<Order>> Get()
        {
            return Ok(_orderService.GetOrders());
        }

        [HttpPost]
        public ActionResult<List<Order>> AddOrder(Order request)
        {
            _orderService.AddOrder(request);
            return Ok(_orderService.GetOrders());
        }

        [HttpPut]
        public ActionResult<List<Order>> UpdateOrder(Order request)
        {
            _orderService.UpdateOrder(request);
            return Ok(_orderService.GetOrders());
        }

        [HttpDelete]
        public ActionResult<List<Order>> DeleteOrder(Guid id)
        {
            _orderService.DeleteOrder(id);
            return Ok(_orderService.GetOrders());
        }























        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(Guid orderId)
        {
            var errorResponse = new ServiceResponse<string>();


            if (_context.Orders == null)
            {
                errorResponse.Success = false;
                errorResponse.Message = "Order not found!";
                return BadRequest(errorResponse);

            }
            var _order = _context.Orders.FirstOrDefault(n => n.OrderId == orderId);
            if (_order == null)
            {
                errorResponse.Success = false;
                errorResponse.Message = "Order doesn't exists!";
                return BadRequest(errorResponse);
            }




            return Ok(_order);
        }

    }
}
