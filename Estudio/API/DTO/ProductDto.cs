namespace Estudio.API.DTO
{
    public record ProductDto(
        string BrandName,
        string ProductName,
        string FragranceType,
        decimal Price,
        bool IsOutOfStock,
        string Gender,
        decimal? DiscountPercentage,
        bool IsNew,
        string? ImageUrl);
}
