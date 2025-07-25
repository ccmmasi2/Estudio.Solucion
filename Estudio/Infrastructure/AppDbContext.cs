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
                builder.Property(p => p.Id).ValueGeneratedOnAdd();

                builder.Property(p => p.ProductName)
                                .HasMaxLength(50)
                                .IsRequired();

                builder.Property(p => p.ImageUrl)
                                .HasMaxLength(5000);

                builder.Property(p => p.IsOutOfStock)
                                .HasDefaultValue(false);

                builder.Property(p => p.IsNew)
                                .HasDefaultValue(false);

                builder.HasIndex(p => new { p.BrandName, p.ProductName, p.FragranceType, p.Price, p.Gender })
               .IsUnique();
            });
        }
    }
}
