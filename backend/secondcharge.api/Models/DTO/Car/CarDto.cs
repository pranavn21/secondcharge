using System.ComponentModel.DataAnnotations;

namespace secondcharge.api.Models.DTO.Car
{
    public class CarDto
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Make { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public string Model { get; set; }

        [Required]
        [Range(1, 500)]
        public int Efficiency { get; set; }

        [Url]
        public string? ModelImageUrl { get; set; }
    }
}
