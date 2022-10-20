using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Postieri.Data;
using Postieri.Models;

namespace Postieri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

   
        [HttpGet]
        public  ActionResult<List<Vehicle>> GetVehicles()
        {
            return Ok(_vehicleService.GetVehicles());
        }

        [HttpGet("GetAllAvailableVehiclesWithoutDefect")]    
        public ActionResult<List<Vehicle>> GetAllAvailableVehiclesWithoutDefect()
        {
            return Ok(_vehicleService.GetAllAvailableVehiclesWithoutDefect());
        }

        [HttpGet("GetAllVehiclesWithDefect")]
        public ActionResult<List<Vehicle>> GetAllVehiclesWithDefect()
        {
            return Ok(_vehicleService.GetAllVehiclesWithDefect());
        }

    
        [HttpGet("{id}")]
        public ActionResult<Vehicle> GetVehicleById(int id)
        {
            return  Ok(_vehicleService.GetVehicleById(id));
        }

        // PUT: api/Vehicles/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicle(int id, Vehicle vehicle)
        {
            _vehicleService.PutVehicle(id, vehicle);
            return Ok();
        }

        // POST: api/Vehicles
     
        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle(Vehicle vehicle)
        {
           _vehicleService.PostVehicle(vehicle);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
           _vehicleService.DeleteVehicle(id);
            return Ok();
        }

        [HttpPut("ChangeVehicle")]
        public async Task<IActionResult> ChangeVehicleAsync(string email)
        {
           _vehicleService.ChangeVehicleAsync(email);
            return Ok();
        }
    }
}
