using Estudio.Common.DTO;

namespace Estudio.Application.Interface
{
    public interface IProductService
    {
        Task<ProductDto> CreateWithValidationAsync(ProductDto dto);
        Task<List<ProductDto>> GetAllAsync();
        Task<ProductDto?> GetByIdAsync(int id);
    }
}