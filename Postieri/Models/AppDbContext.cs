using Microsoft.EntityFrameworkCore;

namespace Postieri.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Shelf> Shelves { get; set; }
        public DbSet<ShelfSize> ShelfSizes { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
    }
}
