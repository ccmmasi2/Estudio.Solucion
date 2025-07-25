using Estudio.Domain;
using Microsoft.EntityFrameworkCore;

namespace Estudio.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(builder =>
            {
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Id)
                       .ValueGeneratedOnAdd();

                builder.HasIndex(p => new { p.BrandName, p.ProductName, p.FragranceType, p.Price, p.Gender })
               .IsUnique();
            });
        }
    }
}
