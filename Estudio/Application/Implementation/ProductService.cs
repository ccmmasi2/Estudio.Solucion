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
                 .Select(x => new ProductDto
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Price = x.Price,
                     IsOutOfStock = x.IsOutOfStock,
                     Gender = x.Gender,
                     DiscountPercentage = x.DiscountPercentage,
                     IsNew = x.IsNew,
                     ImageUrl = x.ImageUrl,
                     PresentationMM = x.PresentationMM,

                     BrandId = x.BrandId,
                     BrandName = x.Brand.Name,

                     FragranceTypeId = x.FragranceTypeId,
                     FragranceTypeName = x.FragranceType.Name
                 }).ToListAsync();

            return dtos;
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            return await _db.Products
                .Where(x => x.Id == id)
                .Select(x => new ProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    IsOutOfStock = x.IsOutOfStock,
                    Gender = x.Gender,
                    DiscountPercentage = x.DiscountPercentage,
                    IsNew = x.IsNew,
                    ImageUrl = x.ImageUrl,
                    PresentationMM = x.PresentationMM,

                    BrandId = x.BrandId,
                    BrandName = x.Brand.Name,

                    FragranceTypeId = x.FragranceTypeId,
                    FragranceTypeName = x.FragranceType.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task<ProductDto> CreateWithValidationAsync(ProductDto dto)
        {
            var brand = await _db.Brands.FindAsync(dto.BrandId);
            if (brand == null)
                throw new Exception("Brand does not exist.");

            var fragranceType = await _db.FragranceTypes.FindAsync(dto.FragranceTypeId);
            if (fragranceType == null)
                throw new Exception("Fragrance Type does not exist.");

            var exists = await _db.Products.AnyAsync(x =>
                                                        x.BrandId == dto.BrandId &&
                                                        x.FragranceTypeId == dto.FragranceTypeId &&
                                                        x.Name.ToLower() == dto.Name.ToLower() &&
                                                        x.Gender == dto.Gender);

            //Pattern matching
            if(dto.Price is 0)
                throw new Exception("Price should be bigger than zero");

            if (exists) throw new InvalidOperationException("Product already exists");

            var product = new Product(dto.Name, dto.Price, dto.IsOutOfStock, dto.Gender,
                                        dto.DiscountPercentage, dto.IsNew, dto.ImageUrl, 
                                        dto.PresentationMM, dto.BrandId, dto.FragranceTypeId);

            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            var resultDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                IsOutOfStock = product.IsOutOfStock,
                Gender = product.Gender,
                DiscountPercentage = product.DiscountPercentage,
                IsNew = product.IsNew,
                ImageUrl = product.ImageUrl,
                PresentationMM = product.PresentationMM,

                BrandId = product.BrandId,
                BrandName = product.Brand.Name,

                FragranceTypeId = product.FragranceTypeId,
                FragranceTypeName = product.FragranceType.Name
            };
            return resultDto;
        }
    }
}


//public async Task<List<Product>> GetByCategoryAsync(string category)
//{
//    return await _db.Products
//        .Where(p => x.Category == category && x.Price < 500)
//        .OrderBy(p => x.Price)
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