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
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id)
                    .ValueGeneratedOnAdd();

                builder.Property(x => x.Name)
                    .HasMaxLength(50)
                    .IsRequired();

                builder.Property(x => x.Price)
                    .IsRequired();

                builder.Property(x => x.Gender)
                    .HasConversion<string>()
                    .HasMaxLength(10)
                    .IsRequired();

                builder.Property(x => x.ImageUrl)
                    .HasMaxLength(5000);

                builder.Property(x => x.IsOutOfStock)
                    .HasDefaultValue(false);

                builder.Property(x => x.IsNew)
                    .HasDefaultValue(false);

                builder.Property(x => x.PresentationMM)
                    .IsRequired();

                builder.Property(x => x.BrandId)
                    .IsRequired();

                builder.Property(x => x.FragranceTypeId)
                    .IsRequired();

                builder.HasOne(x => x.Brand)
                    .WithMany(x => x.Products)
                    .HasForeignKey(x => x.BrandId)
                    .OnDelete(DeleteBehavior.Restrict);

                builder.HasOne(x => x.FragranceType)
                    .WithMany(x => x.Products)
                    .HasForeignKey(x => x.FragranceTypeId)
                    .OnDelete(DeleteBehavior.Restrict);

                builder.HasIndex(x => new { x.BrandId, x.FragranceTypeId, x.Name, x.Price, x.Gender })
                    .IsUnique();
            });

            modelBuilder.Entity<Brand>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id)
                    .ValueGeneratedOnAdd();

                builder.Property(x => x.Name)
                    .HasMaxLength(50)
                    .IsRequired();

                builder.Property(x => x.Description)
                    .HasMaxLength(1000);

                builder.HasIndex(x => x.Name)
                    .IsUnique();
            });

            modelBuilder.Entity<FragranceType>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id)
                    .ValueGeneratedOnAdd();

                builder.Property(x => x.Name)
                    .HasMaxLength(50)
                    .IsRequired();

                builder.Property(x => x.Description)
                    .HasMaxLength(1000);

                builder.HasIndex(x => x.Name)
                    .IsUnique();
            });
        }
    }
}
