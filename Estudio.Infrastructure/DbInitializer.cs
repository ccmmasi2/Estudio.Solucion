using Estudio.Domain;
using Estudio.Infrastructure.SeedData.SeedDTO;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Estudio.Infrastructure
{
    public class DbInitializer
    {
        private readonly AppDbContext _db;

        public DbInitializer(AppDbContext db)
        {
            _db = db;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Any())
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Migration failed", ex);
            }

            SeedBrands();
            SeedFragranceTypes();

            if (_db.ChangeTracker.HasChanges())
                _db.SaveChanges();
        }

        private void SeedBrands()
        {
            if (_db.Brands.Any()) return;

            var filePath = Path.Combine(AppContext.BaseDirectory, "SeedData", "brands.json");
            var json = File.ReadAllText(filePath);
            var brands = JsonSerializer.Deserialize<List<BrandSeedDto>>(json);

            if (brands is not null)
            {
                var brandEntities = brands.Select(b => new Brand(b.Name, b.Description)).ToList();
                _db.Brands.AddRange(brandEntities);
            }
        }

        private void SeedFragranceTypes()
        {
            if (_db.FragranceTypes.Any()) return;

            var filePath = Path.Combine(AppContext.BaseDirectory, "SeedData", "fragranceTypes.json");
            var json = File.ReadAllText(filePath);
            var fragranceTypes = JsonSerializer.Deserialize<List<FragranceTypeSeedDto>>(json);

            if (fragranceTypes is not null)
            {
                var typeEntities = fragranceTypes.Select(f => new FragranceType(f.Name, f.Description)).ToList();
                _db.FragranceTypes.AddRange(typeEntities);
            }
        }
    }
}
