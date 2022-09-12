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
    public class ShelvesController : ControllerBase
    {
        private readonly DataContext _context;

        public ShelvesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Shelves
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shelf>>> GetShelves()
        {
          if (_context.Shelves == null)
          {
              return NotFound();
          }
            return await _context.Shelves.ToListAsync();
        }

        // GET: api/Shelves/5
        [HttpGet("{id}")]
      
        public async Task<ActionResult<Shelf>> GetShelfl(int id)
        {
            if (_context.Shelves == null)
            {
                return NotFound();
            }

            var _shelf = _context.Shelves.Where(n => n.ShelfId == id).Select(shelf => new Shelf()
            {
                WarehouseId = shelf.WarehouseId,
                BinLetter = shelf.BinLetter,
                MaxProducts = shelf.MaxProducts,
                ShelfId = shelf.ShelfId,
                
            }).FirstOrDefault();

            if (_shelf == null)
            {
                return NotFound();
            }

            return _shelf;
        }

        // PUT: api/Shelves/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShelf(int id, ShelfVM shelf)
        {
            if (id != shelf.ShelfId)
            {
                return BadRequest();
            }

            var _shelf = _context.Shelves.FirstOrDefault(n => n.ShelfId == id);
            if (_shelf != null)
            {
                _shelf.BinLetter = shelf.BinLetter;
                _shelf.MaxProducts = shelf.MaxProducts;
                _shelf.WarehouseId = shelf.WarehouseId;


                _context.SaveChanges();
            }

            return NoContent();
        }

        // POST: api/Shelves
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Shelf>> PostShelf(ShelfVM shelf)
        {
            if (_context.Shelves == null)
            {
                return Problem("Entity set 'DataContext.Shelves'  is null.");
            }
            

            var _shelf = new Shelf()
            {
                ShelfId = shelf.ShelfId,
                WarehouseId = shelf.WarehouseId,
                BinLetter = shelf.BinLetter,
                MaxProducts = shelf.MaxProducts
          
            };
            _context.Shelves.Add(_shelf);
            _context.SaveChanges();

            return CreatedAtAction("GetShelf", new { id = shelf.ShelfId }, shelf);
        }

        // DELETE: api/Shelves/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShelf(int id)
        {
            if (_context.Shelves == null)
            {
                return NotFound();
            }
            var shelf = await _context.Shelves.FindAsync(id);
            if (shelf == null)
            {
                return NotFound();
            }

            _context.Shelves.Remove(shelf);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShelfExists(int id)
        {
            return (_context.Shelves?.Any(e => e.ShelfId == id)).GetValueOrDefault();
        }
    }
}
