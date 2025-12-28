using System.ComponentModel.DataAnnotations;

namespace secondcharge.api.Models.DTO.Car
{
    public class AddCarRequestDto
    {
        [Required(ErrorMessage = "Make is required")]
        [MinLength(2, ErrorMessage = "The make must be at least 2 characters")]
        [MaxLength(50, ErrorMessage = "The make cannot exceed 50 characters")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Model is required")]
        [MinLength(1, ErrorMessage = "The model must be at least 1 character")]
        [MaxLength(100, ErrorMessage = "The model cannot exceed 100 characters")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Efficiency is required")]
        [Range(1, 500, ErrorMessage = "Efficiency must be between 1 and 500 MPGe")]
        public int Efficiency { get; set; }

        [Url(ErrorMessage = "Model image URL must be a valid URL")]
        public string? ModelImageUrl { get; set; }
    }
}
