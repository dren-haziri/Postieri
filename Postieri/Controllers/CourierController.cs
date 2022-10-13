using System;
using Microsoft.AspNetCore.Mvc;
using Postieri.DTOs;

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
    }
}
