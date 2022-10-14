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
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
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
        public ActionResult<List<Order>> DeleteOrder(Guid OrderId)
        {
            _orderService.DeleteOrder(OrderId);
            return Ok(_orderService.GetOrders());
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetOrderById(Guid OrderId)
        {
            return Ok(_orderService.GetOrderById(OrderId));
        }

        [HttpPut("UpdateStatusOfOrder")]
        public ActionResult<List<Order>> setStatus(Guid orderId, string status)
        {
            _orderService.setStatus(orderId, status);
            return Ok();
        }

        [HttpPut("assignCourierToOrder")]
        public ActionResult<List<Order>> assignCourierToOrder(Guid orderId, Guid courierId)
        {
            _orderService.assignCourierToOrder(orderId, courierId);
            return Ok();
        }
    }
}
