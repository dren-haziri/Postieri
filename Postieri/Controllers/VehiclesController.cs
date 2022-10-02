﻿using System;
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
        private readonly DataContext _context;

        public VehiclesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Vehicles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            if (_context.Vehicles == null)
            {
                return NotFound();
            }
            return await _context.Vehicles.ToListAsync();
        }

        [HttpGet("GetAllAvailableVehiclesWithoutDefect")]
       
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetAllAvailableVehiclesWithoutDefect()
        {
            if (_context.Vehicles == null)
            {
                return NotFound();
            }
            return await _context.Vehicles.Where(x => x.IsAvailable==true && x.HasDefect==false).ToListAsync();
        }

        [HttpGet("GetAllVehiclesWithDefect")]

        public async Task<ActionResult<IEnumerable<Vehicle>>> GetAllVehiclesWithDefect()
        {
            if (_context.Vehicles == null)
            {
                return NotFound();
            }
                return await _context.Vehicles.Where(x => x.HasDefect == true).ToListAsync();
            
        }

        // GET: api/Vehicles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(int id)
        {
            if (_context.Vehicles == null)
            {
                return NotFound();
            }
            var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return vehicle;
        }

        // PUT: api/Vehicles/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicle(int id, Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return BadRequest();
            }
            var _vehicle = _context.Vehicles.FirstOrDefault(n => n.Id == id);
            if (_vehicle != null)
            {
                _vehicle.Height = vehicle.Height;
                _vehicle.Length = vehicle.Length;
                _vehicle.Width = vehicle.Width;
                _vehicle.LoadSpace = vehicle.Height * vehicle.Width * vehicle.Length;
                _vehicle.LoadWeight = vehicle.LoadWeight;
                _vehicle.CourierId = vehicle.CourierId;
                _vehicle.Description = vehicle.Description;
                _vehicle.HasDefect = vehicle.HasDefect;
                _vehicle.IsAvailable = vehicle.IsAvailable;
                _vehicle.Type = vehicle.Type;
                _vehicle.PlateNumber = vehicle.PlateNumber;
                await _context.SaveChangesAsync();
            }


            return NoContent();
        }

        // POST: api/Vehicles
     
        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle(Vehicle vehicle)
        {
            if (_context.Vehicles == null)
            {
                return Problem("Entity set 'DataContext.Vehicles'  is null.");
            }
            var _vehicle = new Vehicle()
            {
                Id = vehicle.Id,
                Height = vehicle.Height,
                Length = vehicle.Length,
                Width = vehicle.Width,
                LoadSpace = vehicle.Height * vehicle.Width * vehicle.Length,
                LoadWeight = vehicle.LoadWeight,
                CourierId = vehicle.CourierId,
                Description = vehicle.Description,
                HasDefect = vehicle.HasDefect,
                IsAvailable = vehicle.IsAvailable,
                Type = vehicle.Type,
                PlateNumber = vehicle.PlateNumber,

            };
            _context.Vehicles.Add(_vehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicle", new { id = vehicle.Id }, vehicle);
        }

        // DELETE: api/Vehicles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            if (_context.Vehicles == null)
            {
                return NotFound();
            }
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehicleExists(int id)
        {
            return (_context.Vehicles?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private bool VehicleInUseHasDefect(int id)
        {
            var vehicle = _context.Vehicles.Find(id);

            return vehicle.HasDefect == true;
        }

        private int GetDefaultAvailableVehicle()
        {
            return _context.Vehicles.Where(x => x.IsAvailable == true && x.HasDefect == false).FirstOrDefault().Id;
        }

        [HttpPut("ChangeVehicle")]
        public async Task<IActionResult> ChangeVehicleAsync(string email)
        {
            var user = await _context.Couriers.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
          
            if (user == null)
            {
                return NotFound();
            }
            if (VehicleInUseHasDefect(user.VehicleId))
            {
                user.VehicleId =  GetDefaultAvailableVehicle();
                await _context.SaveChangesAsync();
            }

            return NoContent();


        }



    }
}
