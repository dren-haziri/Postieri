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
        public DbSet<Role> Roles { get; set; }
        public DbSet<Dimension> Dimensions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Shelf> Shelves { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<ClientOrder> ClientOrders { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<LiveAgent> LiveAgents { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LiveAgent>().ToTable("LiveAgents");

        }
    }
}
