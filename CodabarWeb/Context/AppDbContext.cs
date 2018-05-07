using CodabarWeb.Configurations;
using CodabarWeb.Models;
using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace CodabarWeb.Context
{
    public sealed class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Unit> Units { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UnitEntityTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
