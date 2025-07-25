using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Estudio.API.DTO
{
    public record ProductDto
    {
        public int Id { get; init; }

        [Required, MaxLength(50)]
        public string Name { get; init; } = default!;

        [Required, MaxLength(30)]
        public string FragranceType { get; init; } = default!;
        public decimal Price { get; init; }

        [DefaultValue(false)]
        public bool IsOutOfStock { get; init; }

        [Required]
        public string Gender { get; init; } = default!;
        public decimal? DiscountPercentage { get; init; }

        [DefaultValue(false)]
        public bool IsNew { get; init; }

        [DefaultValue("No image yet")]
        public string? ImageUrl { get; init; }

        [Required]
        public int BrandId { get; init; }
        public string? BrandName { get; init; }

        [Required, DefaultValue(100)]
        public int PresentationMM { get; init; }
    }
}
