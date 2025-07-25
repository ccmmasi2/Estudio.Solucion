using Estudio.API.DTO;
using Estudio.Application.Interface;
using Estudio.Domain;
using Estudio.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Estudio.Application.Implementation
{
    public class BrandService : IBrandService
    {
        private readonly AppDbContext _db;

        public BrandService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<BrandDto>> GetAllAsync()
        {
            var dtos = await _db.Brands
                 .Select(x => new BrandDto
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Description = x.Description
                 }).ToListAsync();
            return dtos;
        }

        public async Task<BrandDto?> GetByIdAsync(int id)
        {
            return await _db.Brands
                .Where(x => x.Id == id)
                .Select(x => new BrandDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                })
                .FirstOrDefaultAsync();
        }

        public async Task<BrandDto> CreateWithValidationAsync(BrandDto dto)
        {
            var exists = await _db.Brands.AnyAsync(x =>
                                                    x.Name.ToLower() == dto.Name.ToLower());

            if (exists) throw new InvalidOperationException("Brand already exists");

            var brand = new Brand(dto.Name, dto.Description);

            _db.Brands.Add(brand);
            await _db.SaveChangesAsync();

            var resultDto = new BrandDto
            {
                Id = brand.Id,
                Name = brand.Name,
                Description = brand.Description
            };
            return resultDto;
        }
    }
}


//public async Task<List<Product>> GetByCategoryAsync(string category)
//{
//    return await _db.Products
//        .Where(p => x.Category == category && x.Price < 500)
//        .OrderBy(p => x.Price)
//        .ToListAsync();
//}

//public async Task<Product?> ApplyDiscountAsync(Guid id, decimal percentage)
//{
//    var product = await _db.Products.FindAsync(id);
//    if (product == null) return null;

//    var discounted = product.ApplyDiscount(percentage);
//    _db.Entry(product).CurrentValues.SetValues(discounted);
//    await _db.SaveChangesAsync();
//    return discounted;
//}