using Microsoft.EntityFrameworkCore;
using Postieri.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Postieri.Data
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext>options):base(options)
        {

        }
        public DbSet<Roles> Roles { get; set; }
    }
}
