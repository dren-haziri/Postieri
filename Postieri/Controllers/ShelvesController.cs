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
    public class ShelvesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

      

        public ShelvesController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
           
        }

        // GET: api/Shelves
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShelfWarehouseDto>>> GetShelves()
        {
            var shelves = await _context.Shelves.Include(sh => sh.Warehouse).Include(sh => sh.Products).ToListAsync();
            if (_context.Shelves == null)
          {
              return NotFound();
          }
            return Ok(_mapper.Map<IEnumerable<ShelfWarehouseDto>>(shelves));
        }

        // GET: api/Shelves/5
        [HttpGet("{id}")]
      
        public async Task<ActionResult<ShelfWarehouseDto>> GetShelfl(int id)
        {
            if (_context.Shelves == null)
            {
                return NotFound();
            }

            var _shelf = _context.Shelves.Where(n => n.ShelfId == id).Include(sh => sh.Warehouse).Include(sh=>sh.Products).FirstOrDefault();

            if (_shelf == null)
            {
                return NotFound();
            }

           return _mapper.Map<ShelfWarehouseDto>(_shelf);
        }

        // PUT: api/Shelves/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShelf(int id, ShelfDto shelf)
        {
            if (id != shelf.ShelfId)
            {
                return BadRequest();
            }

            var _shelf = _context.Shelves.FirstOrDefault(n => n.ShelfId == id);
            if (_shelf != null)
            {
                _mapper.Map(shelf, _shelf);
                _context.SaveChanges();
            }

            return NoContent();
        }

        // POST: api/Shelves
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Shelf>> PostShelf(ShelfDto shelf)
        {
            if (_context.Shelves == null)
            {
                return Problem("Entity set 'DataContext.Shelves'  is null.");
            }

            var _shelf = new Shelf();
            _mapper.Map(shelf, _shelf);
            _context.Shelves.Add(_shelf);
            _context.SaveChanges();
            return CreatedAtAction(nameof(PostShelf), new { id = shelf.ShelfId }, shelf);

        }
        [HttpPost("add-product-to-shelf")]
        public string AddProductToShelf(ProductDto products)
        {

            var product = new Product();
            _mapper.Map(products, product);
            
            var shelf = _context.Shelves
                .Find(products.ShelfId);

            if (shelf.AvailableSlots < 1)
            {
                return "Shelf is full!";
            }

            _context.Products
                .Add(product);

            shelf.AvailableSlots--;

            _context.SaveChanges();
            return "Product was stored successfully!";

        }
        [HttpPost("remove-product-from-shelf")]
        public void RemoveProductFromShelf(Guid product)
        {
            var _product = _context.Products
                .Find(product);  

            var shelf = _context.Shelves
                .Find(_product.ShelfId);

            _product.ShelfId = 0 ;
            if(shelf != null)
            {
                shelf.AvailableSlots++;
            }
            _context.SaveChanges();

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
