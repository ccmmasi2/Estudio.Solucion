using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Estudio.Contracts.Enums.Enumerators;

namespace Estudio.Contracts.DTO
{
    public record ProductDto
    {
        public int Id { get; init; }

        [Required, MaxLength(50)]
        public string Name { get; init; } = default!;

        public decimal Price { get; init; }

        [DefaultValue(false)]
        public bool IsOutOfStock { get; init; }

        [Required]
        public Gender Gender { get; init; } = default!;
        public decimal? DiscountPercentage { get; init; }

        [DefaultValue("No image yet")]
        public string? ImageUrl { get; init; }

        [Required, DefaultValue(100)]
        public int PresentationMM { get; init; }

        [Required]
        public int BrandId { get; init; }
        public string? BrandName { get; init; }

        [Required]
        public int FragranceTypeId { get; init; }
        public string? FragranceTypeName { get; init; }
    }
}
