using Estudio.Domain;
using Microsoft.EntityFrameworkCore;

namespace Estudio.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Brand> Brands => Set<Brand>();
        public DbSet<FragranceType> FragranceTypes => Set<FragranceType>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(builder =>
            {
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Id)
                    .ValueGeneratedOnAdd();

                builder.Property(p => p.Name)
                    .HasMaxLength(50)
                    .IsRequired();

                builder.Property(p => p.FragranceType)
                    .IsRequired();

                builder.Property(p => p.Price)
                    .IsRequired();

                builder.Property(p => p.Gender)
                    .IsRequired();

                builder.Property(p => p.ImageUrl)
                    .HasMaxLength(5000);

                builder.Property(p => p.IsOutOfStock)
                    .HasDefaultValue(false);

                builder.Property(p => p.IsNew)
                    .HasDefaultValue(false);

                builder.Property(p => p.PresentationMM)
                    .IsRequired();

                builder.Property(p => p.BrandId)
                    .IsRequired();

                builder.HasOne(p => p.Brand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(p => p.BrandId)
                    .OnDelete(DeleteBehavior.Restrict);

                builder.HasIndex(p => new { p.BrandId, p.Name, p.FragranceType, p.Price, p.Gender })
               .IsUnique();
            });

            modelBuilder.Entity<Brand>(builder =>
            {
                builder.HasKey(b => b.Id);
                builder.Property(p => p.Id)
                    .ValueGeneratedOnAdd();

                builder.Property(b => b.Name)
                    .HasMaxLength(50)
                    .IsRequired();

                builder.Property(b => b.Description)
                    .HasMaxLength(1000);

                builder.HasIndex(b => b.Name)
                    .IsUnique();
            });

            modelBuilder.Entity<FragranceType>(builder =>
            {
                builder.HasKey(b => b.Id);
                builder.Property(p => p.Id)
                    .ValueGeneratedOnAdd();

                builder.Property(b => b.Name)
                    .HasMaxLength(50)
                    .IsRequired();

                builder.Property(b => b.Description)
                    .HasMaxLength(1000);

                builder.HasIndex(b => b.Name)
                    .IsUnique();
            });
        }
    }
}
