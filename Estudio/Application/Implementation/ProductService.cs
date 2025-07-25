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

        public async Task<Product> CreateAsync(Product product)
        {
            var exists = await _db.Products.AnyAsync(x =>
                                                        x.BrandName == product.BrandName &&
                                                        x.ProductName == product.ProductName &&
                                                        x.Gender == product.Gender &&
                                                        x.FragranceType == product.FragranceType);

            if (exists) throw new InvalidOperationException("Product already exists");

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