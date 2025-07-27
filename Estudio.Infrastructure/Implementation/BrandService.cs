using Estudio.Application.Exceptions;
using Estudio.Application.Interface;
using Estudio.Contracts.DTO;
using Estudio.Domain;
using Microsoft.EntityFrameworkCore;

namespace Estudio.Infrastructure.Implementation
{
    public class BrandService : IBrandService
    {
        private readonly AppDbContext _db;

        public BrandService(AppDbContext db, ITraceLogger logger)
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
            var brand = await _db.Brands
                .Where(x => x.Id == id)
                .Select(x => new BrandDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                })
                .FirstOrDefaultAsync();

            if (brand == null)
                throw new NotFoundException($"Brand with ID {id} not found.");

            return brand;
        }

        public async Task<BrandDto> CreateWithValidationAsync(BrandDto dto)
        {
            var exists = await _db.Brands.AnyAsync(x =>
                                                    x.Name.ToLower() == dto.Name.ToLower());

            if (exists) 
                throw new InvalidOperationException("Brand with Name {dto.Name} already exists.");

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
