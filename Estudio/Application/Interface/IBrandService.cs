using Estudio.API.DTO;
using Estudio.Domain;

namespace Estudio.Application.Interface
{
    public interface IBrandService
    {
        Task<Brand> CreateWithValidationAsync(BrandDto dto);
        Task<List<Brand>> GetAllAsync();
        Task<Brand?> GetByIdAsync(int id);
    }
}