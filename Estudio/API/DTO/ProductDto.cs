namespace Estudio.API.DTO
{
    public record ProductDto(
        int BrandId,
        string Name,
        string FragranceType,
        decimal Price,
        bool IsOutOfStock,
        string Gender,
        decimal? DiscountPercentage,
        bool IsNew,
        string? ImageUrl);
}
