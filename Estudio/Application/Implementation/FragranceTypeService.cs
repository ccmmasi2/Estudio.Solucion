using Estudio.API.DTO;
using Estudio.Application.Interface;
using Estudio.Domain;
using Estudio.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Estudio.Application.Implementation
{
    public class FragranceTypeService : IFragranceTypeService
    {
        private readonly AppDbContext _db;

        public FragranceTypeService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<FragranceTypeDto>> GetAllAsync()
        {
            var dtos = await _db.FragranceTypes
                 .Select(b => new FragranceTypeDto
                 {
                     Id = b.Id,
                     Name = b.Name,
                     Description = b.Description
                 }).ToListAsync();
            return dtos;
        }

        public async Task<FragranceTypeDto?> GetByIdAsync(int id)
        {
            return await _db.FragranceTypes
                .Where(b => b.Id == id)
                .Select(b => new FragranceTypeDto
                {
                    Id = b.Id,
                    Name = b.Name,
                    Description = b.Description
                })
                .FirstOrDefaultAsync();
        }

        public async Task<FragranceTypeDto> CreateWithValidationAsync(FragranceTypeDto dto)
        {
            var exists = await _db.FragranceTypes.AnyAsync(x =>
                                                    x.Name.ToLower() == dto.Name.ToLower());

            if (exists) throw new InvalidOperationException("FragranceType already exists");

            var fragranceType = new FragranceType(dto.Name, dto.Description);

            _db.FragranceTypes.Add(fragranceType);
            await _db.SaveChangesAsync();

            var resultDto = new FragranceTypeDto
            {
                Id = fragranceType.Id,
                Name = fragranceType.Name,
                Description = fragranceType.Description
            };
            return resultDto;
        } 
    }
}


//public async Task<List<Product>> GetByCategoryAsync(string category)
//{
//    return await _db.Products
//        .Where(p => p.Category == category && p.Price < 500)
//        .OrderBy(p => p.Price)
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