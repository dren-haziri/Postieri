using Microsoft.EntityFrameworkCore;
using Postieri.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Postieri.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Dimension> Dimensions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Shelf> Shelves { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
    }
}
