
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Postieri.Data;
using Postieri.Models;
using Postieri.ViewModels;

namespace Postieri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        private readonly DataContext _context;

        public WarehousesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Warehouses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Warehouse>>> GetWarehouse()
        {
            if (_context.Warehouse == null)
            {
                return NotFound();
            }
            return await _context.Warehouse.AsNoTracking().Include(w => w.Shelves).ToListAsync();
        }

        // GET: api/Warehouses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Warehouse>> GetWarehouse(int id)
        {
            if (_context.Warehouse == null)
            {
                return NotFound();
            }

            var _warehouse = _context.Warehouse.Where(n => n.WarehouseId == id).Select(warehouse => new Warehouse()
            {
                WarehouseId = warehouse.WarehouseId,
                Location = warehouse.Location,
                Area = warehouse.Area,
                Name = warehouse.Name,
                NumOfShelves = warehouse.NumOfShelves,
                Shelves = warehouse.Shelves
             
            }).FirstOrDefault();

            if (_warehouse == null)
            {
                return NotFound();
            }

            return _warehouse;
        }

        // PUT: api/Warehouses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWarehouse(int id, WarehouseVM warehouse)
        {

            var _warehouse = _context.Warehouse.FirstOrDefault(n => n.WarehouseId == id);
            if (_warehouse != null)
            {
                _warehouse.Area = warehouse.Area;
                _warehouse.NumOfShelves = warehouse.NumOfShelves;
                _warehouse.Name = warehouse.Name;
                _warehouse.Location = warehouse.Location;


                _context.SaveChanges();
            }

            return NoContent();
        }

        // POST: api/Warehouses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Warehouse>> PostWarehouse(WarehouseVM warehouse)
        {
            if (_context.Warehouse == null)
            {
                return Problem("Entity set 'DataContext.Shelves'  is null.");
            }
         

            var _warehouse = new Warehouse()
            {
                Name = warehouse.Name,
                Area = warehouse.Area,
                Location = warehouse.Location,
                NumOfShelves = warehouse.NumOfShelves,

            };
            _context.Warehouse.Add(_warehouse);
            _context.SaveChanges();
            return CreatedAtAction("GetWarehouse", new { id = warehouse.WarehouseId }, warehouse);

        }

        // DELETE: api/Warehouses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarehouse(int id)
        {
            if (_context.Warehouse == null)
            {
                return NotFound();
            }
            var warehouse = await _context.Warehouse.FindAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }

            _context.Warehouse.Remove(warehouse);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WarehouseExists(int id)
        {
            return (_context.Warehouse?.Any(e => e.WarehouseId == id)).GetValueOrDefault();
        }
    }
}