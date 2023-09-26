using assignment.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2
{
    public class CarDBContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public CarDBContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(Configuration.GetConnectionString("CarDb"));
        }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }

    }
}

