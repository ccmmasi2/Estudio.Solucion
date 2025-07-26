using Estudio.Application.Exceptions;
using Estudio.Application.Interface;
using Estudio.Contracts.DTO;
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
                 .Select(x => new FragranceTypeDto
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Description = x.Description
                 }).ToListAsync();
            return dtos;
        }

        public async Task<FragranceTypeDto?> GetByIdAsync(int id)
        {
            var fragranceType = await _db.FragranceTypes
                .Where(x => x.Id == id)
                .Select(x => new FragranceTypeDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                })
                .FirstOrDefaultAsync();

            if (fragranceType == null)
                throw new NotFoundException($"FragranceType with ID {id} not found.");

            return fragranceType;
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
