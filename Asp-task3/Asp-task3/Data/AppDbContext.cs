using Asp_task3.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Asp_task3.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ShopCategory> ShopCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Brand> Brands { get; set; }

    }
}
