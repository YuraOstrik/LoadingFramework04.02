using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loading.Framework29._01.Basic_class
{
    internal class AppDbContext : DbContext
    {
        public DbSet<Stationery> Stationeries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Sale> Sales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-N63S67G\SQLEXPRESS;Database=thirdDb;Integrated Security=SSPI;TrustServerCertificate=true");
        }
    }
}
