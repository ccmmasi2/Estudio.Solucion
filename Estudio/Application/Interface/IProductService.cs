using Estudio.Domain;

namespace Estudio.Application.Interface
{
    public interface IProductService
    {
        Task<Product> CreateAsync(Product product);
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
    }
}