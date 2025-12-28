using System.ComponentModel.DataAnnotations;

namespace secondcharge.api.Models.DTO.Car
{
    public class AddCarRequestDto
    {
        [Required]
        [MinLength(2, ErrorMessage = "The make has to be a minimum of 3 characters.")]
        [MaxLength(50, ErrorMessage = "The make has to be a maximum of 50 characters.")]
        public string Make { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "The model has to be a minimum of 1 character.")]
        [MaxLength(100, ErrorMessage = "The model has to be a maximum of 100 characters.")]
        public string Model { get; set; }

        [Required]
        public int Efficiency { get; set; }

        public string? ModelImageUrl { get; set; }
    }
}
