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

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var dtos = await _db.Products
                 .Select(b => new ProductDto
                 {
                     Id = b.Id,
                     Name = b.Name,
                     FragranceType = b.FragranceType,
                     Price = b.Price,
                     IsOutOfStock = b.IsOutOfStock,
                     Gender = b.Gender,
                     DiscountPercentage = b.DiscountPercentage,
                     IsNew = b.IsNew,
                     ImageUrl = b.ImageUrl,
                     PresentationMM = b.PresentationMM,
                     BrandId = b.BrandId,
                     BrandName = b.Brand.Name
                 }).ToListAsync();

            return dtos;
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            return await _db.Products
                .Where(p => p.Id == id)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    FragranceType = p.FragranceType,
                    Price = p.Price,
                    IsOutOfStock = p.IsOutOfStock,
                    Gender = p.Gender,
                    DiscountPercentage = p.DiscountPercentage,
                    IsNew = p.IsNew,
                    ImageUrl = p.ImageUrl,
                    PresentationMM = p.PresentationMM,
                    BrandId = p.BrandId,
                    BrandName = p.Brand.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task<ProductDto> CreateWithValidationAsync(ProductDto dto)
        {
            var brand = await _db.Brands.FindAsync(dto.BrandId);
            if (brand == null)
                throw new Exception("Brand does not exist.");

            var exists = await _db.Products.AnyAsync(x =>
                                                        x.BrandId == dto.BrandId &&
                                                        x.Name.ToLower() == dto.Name.ToLower() &&
                                                        x.Gender == dto.Gender &&
                                                        x.FragranceType == dto.FragranceType);

            if (exists) throw new InvalidOperationException("Product already exists");

            var product = new Product(dto.BrandId, dto.Name, dto.FragranceType, dto.Price, dto.IsOutOfStock, dto.Gender,
            dto.DiscountPercentage, dto.IsNew, dto.ImageUrl, dto.PresentationMM);

            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            var resultDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                FragranceType = product.FragranceType,
                Price = product.Price,
                IsOutOfStock = product.IsOutOfStock,
                Gender = product.Gender,
                DiscountPercentage = product.DiscountPercentage,
                IsNew = product.IsNew,
                ImageUrl = product.ImageUrl,
                PresentationMM = product.PresentationMM,
                BrandId = product.BrandId,
                BrandName = product.Brand.Name
            };
            return resultDto;
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