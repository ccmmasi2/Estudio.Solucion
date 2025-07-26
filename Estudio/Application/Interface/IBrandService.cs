using Estudio.Common.DTO;

namespace Estudio.Application.Interface
{
    public interface IBrandService
    {
        Task<BrandDto> CreateWithValidationAsync(BrandDto dto);
        Task<List<BrandDto>> GetAllAsync();
        Task<BrandDto?> GetByIdAsync(int id);
    }
}