using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
//using System.Web.Http;
using System.Web;
//using System.Runtime.Remoting.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Postieri.Data;
using Postieri.DTO;
using Postieri.Models;
using Postieri.Services;

namespace Postieri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessIntegrationController : ControllerBase

    {
        private readonly IBusinessIntegrationService _businessIntegration;

        public BusinessIntegrationController(IBusinessIntegrationService businessIntegrationService)
        {
            _businessIntegration = businessIntegrationService;
        }

        [HttpPost("PostOrder")]
        public ActionResult<List<Order>> PostOrder(OrderDto order)
        {
            _businessIntegration.PostOrder(order);
            return Ok();
        }
        [HttpGet("GetAllOrders")]
        public ActionResult<List<Order>> GetAllOrders()
        {
            return Ok(_businessIntegration.GetAllOrders());
        }
        [HttpGet("GetOrderById")]
        public Order GetOrders(Guid id)
        {
            return _businessIntegration.GetOrders(id);
        }

        [HttpPost("SaveBusiness")]
        public async Task<ActionResult<string>> SaveBusiness(BusinessDto request)
        {
            _businessIntegration.SaveBusiness(request);
            return Ok();
        }
    }
}
