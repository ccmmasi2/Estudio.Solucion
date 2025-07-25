using Estudio.API.DTO;
using Estudio.Application.Interface;
using Estudio.Domain;
using Estudio.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Estudio.Application.Implementation
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _db;

        public ProductService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _db.Products.FindAsync(id);
        }

        public async Task<Product> CreateWithValidationAsync(ProductDto dto)
        {
            var brand = await _db.Brands.FindAsync(dto.BrandId);
            if (brand == null)
                throw new Exception("Brand does not exist.");

            var exists = await _db.Products.AnyAsync(x =>
                                                        x.BrandId == dto.BrandId &&
                                                        x.Name == dto.Name &&
                                                        x.Gender == dto.Gender &&
                                                        x.FragranceType == dto.FragranceType);

            if (exists) throw new InvalidOperationException("Product already exists");

            var product = new Product(dto.BrandId, dto.Name, dto.FragranceType, dto.Price, dto.IsOutOfStock, dto.Gender,
            dto.DiscountPercentage, dto.IsNew, dto.ImageUrl);

            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return product;
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