using Estudio.API.DTO;
using Estudio.Domain;

namespace Estudio.Application.Interface
{
    public interface IProductService
    {
        Task<Product> CreateWithValidationAsync(ProductDto dto);
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
    }
}