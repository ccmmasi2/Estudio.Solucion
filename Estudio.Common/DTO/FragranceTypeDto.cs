using System.ComponentModel.DataAnnotations;

namespace Estudio.Common.DTO
{
    public record FragranceTypeDto
    {
        public int Id { get; init; }

        [Required, MaxLength(50)]
        public string Name { get; init; } = default!;


        [MaxLength(1000)]
        public string? Description { get; init; }
    }
}
