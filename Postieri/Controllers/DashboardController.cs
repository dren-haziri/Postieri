using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Postieri.Data;
using Postieri.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Postieri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboard _dashboard;

        public DashboardController(IDashboard dashboard)
        {
            _dashboard = dashboard;
        }


        [HttpGet("GetTotal")]
        public double GetTotal()
        {
            return _dashboard.GetTotal();
        }
        [HttpGet("OrdersOfLastThreeMonths")]
        public int OrdersInLastThreeMonths() { return _dashboard.OrdersInLastThreeMonths(); }

    }
}
