using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Postieri.Models;

namespace Postieri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShelfSizesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ShelfSizesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ShelfSizes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShelfSize>>> GetShelfSizes()
        {
          if (_context.ShelfSizes == null)
          {
              return NotFound();
          }
            return await _context.ShelfSizes.ToListAsync();
        }

        // GET: api/ShelfSizes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShelfSize>> GetShelfSize(int id)
        {
          if (_context.ShelfSizes == null)
          {
              return NotFound();
          }
            var shelfSize = await _context.ShelfSizes.FindAsync(id);

            if (shelfSize == null)
            {
                return NotFound();
            }

            return shelfSize;
        }

        // PUT: api/ShelfSizes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShelfSize(int id, ShelfSize shelfSize)
        {
            if (id != shelfSize.ShelfSizeId)
            {
                return BadRequest();
            }

            _context.Entry(shelfSize).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShelfSizeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ShelfSizes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShelfSize>> PostShelfSize(ShelfSize shelfSize)
        {
          if (_context.ShelfSizes == null)
          {
              return Problem("Entity set 'AppDbContext.ShelfSizes'  is null.");
          }
            _context.ShelfSizes.Add(shelfSize);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShelfSize", new { id = shelfSize.ShelfSizeId }, shelfSize);
        }

        // DELETE: api/ShelfSizes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShelfSize(int id)
        {
            if (_context.ShelfSizes == null)
            {
                return NotFound();
            }
            var shelfSize = await _context.ShelfSizes.FindAsync(id);
            if (shelfSize == null)
            {
                return NotFound();
            }

            _context.ShelfSizes.Remove(shelfSize);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShelfSizeExists(int id)
        {
            return (_context.ShelfSizes?.Any(e => e.ShelfSizeId == id)).GetValueOrDefault();
        }
    }
}
