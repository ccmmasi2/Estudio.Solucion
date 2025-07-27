using Estudio.Application.Exceptions;
using Estudio.Application.Interface;
using Estudio.Contracts.DTO;
using Estudio.Domain;
using Microsoft.EntityFrameworkCore;

namespace Estudio.Infrastructure.Implementation
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
            var product = await _db.Products
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

            if (product == null)
                throw new NotFoundException($"Product with ID {id} not found.");

            return product;
        }

        public async Task<ProductDto> CreateWithValidationAsync(ProductDto dto)
        {
            var brand = await _db.Brands.FindAsync(dto.BrandId);
            if (brand == null)
                throw new NotFoundException($"Brand with ID {dto.BrandId} not found.");

            var fragranceType = await _db.FragranceTypes.FindAsync(dto.FragranceTypeId);
            if (fragranceType == null)
                throw new NotFoundException($"Fragrance Type with ID {dto.FragranceTypeId} not found.");

            var exists = await _db.Products.AnyAsync(x =>
                                                        x.BrandId == dto.BrandId &&
                                                        x.FragranceTypeId == dto.FragranceTypeId &&
                                                        x.Name.ToLower() == dto.Name.ToLower() &&
                                                        x.Gender == dto.Gender);

            //Pattern matching
            if (dto.Price is 0)
                throw new BadRequestException("Price should be bigger than zero.");

            if (exists)
                throw new ConflictException("Product already exists.");

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
