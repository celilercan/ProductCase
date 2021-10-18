using Microsoft.EntityFrameworkCore;
using ProductCase.Data.Entity;

namespace ProductCase.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<CategoryAttribute> CategoryAttribute { get; set; }
        public DbSet<ProductAttribute> productAttribute { get; set; }

    }
}
