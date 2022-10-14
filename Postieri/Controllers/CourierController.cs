using System;
using Microsoft.AspNetCore.Mvc;
using Postieri.DTOs;
using Postieri.Models;

namespace Postieri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourierController : ControllerBase
    {
        public ICourierService _courierService;

        public CourierController(ICourierService courierService)
        {
            _courierService = courierService;
        }

        [HttpPut("changestatus")]
        public ActionResult<List<StatusOrderDto>> ChangeStatus(Guid orderId, string status)
        {
             _courierService.UpdateStatus(orderId, status);
            return Ok();
        }
        [HttpPut("accept-order")]
        public ActionResult<List<Order>> AcceptOrder(Guid order, Guid courierId)
        {
            _courierService.AcceptOrder(order, courierId);
            return Ok();
        }
        [HttpPut("decline-order")]
        public ActionResult<List<Order>> DeclineOrder(Guid orderId)
        {
            _courierService.DeclineOrder(orderId);
            return Ok();
        }

    }
}
