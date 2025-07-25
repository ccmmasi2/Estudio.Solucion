using Estudio.API.DTO;

namespace Estudio.Application.Interface
{
    public interface IFragranceTypeService
    {
        Task<FragranceTypeDto> CreateWithValidationAsync(FragranceTypeDto dto);
        Task<List<FragranceTypeDto>> GetAllAsync();
        Task<FragranceTypeDto?> GetByIdAsync(int id);
    }
}