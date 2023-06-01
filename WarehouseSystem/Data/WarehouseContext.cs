using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using WarehouseSystem.Models;

namespace WarehouseSystem.Data
{
    public class WarehouseContext:IdentityDbContext<ApplicationUsers>
    {
        IConfiguration configuration;
        public WarehouseContext(IConfiguration _configuration)
        {
            configuration= _configuration;
        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Warehouse"));
            base.OnConfiguring(optionsBuilder);

        }
    }
}
