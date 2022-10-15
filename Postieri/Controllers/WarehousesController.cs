
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Postieri.Data;
using Postieri.Models;
//using Postieri.ViewModels;

namespace Postieri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public WarehousesController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
       

        // GET: api/Warehouses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WarehouseShelvesDto>>> GetWarehouse()
        {
            var warehouses = await _context.Warehouse.Include(sh => sh.Shelves).ToListAsync();


            if (_context.Warehouse == null)
            {
                return NotFound();
            }
            
                   return Ok(_mapper.Map<IEnumerable<WarehouseShelvesDto>>(warehouses));
        }

        // GET: api/Warehouses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Warehouse>> GetWarehouse(int id)
        {
            if (_context.Warehouse == null)
            {
                return NotFound();
            }
             
                
            var _warehouse = _context.Warehouse.Where(n => n.WarehouseId == id).Include(w=>w.Shelves).FirstOrDefault();


            if (_warehouse == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WarehouseShelvesDto>(_warehouse));

           
        }

        // PUT: api/Warehouses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWarehouse(int id, WarehouseDto warehouse)
        {

            var _warehouse = _context.Warehouse.FirstOrDefault(n => n.WarehouseId == id);
            if (_warehouse != null)
            {
                _mapper.Map(warehouse, _warehouse);
                _context.SaveChanges();
            }

            return NoContent();
        }

        // POST: api/Warehouses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Warehouse>> PostWarehouse(WarehouseDto warehouse)
        {
            if (_context.Warehouse == null)
            {
                return Problem("Entity set 'DataContext.Shelves'  is null.");
            }
            var _warehouse = new Warehouse();
            _mapper.Map(warehouse, _warehouse);
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