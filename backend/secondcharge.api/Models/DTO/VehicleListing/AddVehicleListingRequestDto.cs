using System.ComponentModel.DataAnnotations;

namespace secondcharge.api.Models.DTO.VehicleListing
{
    public class AddVehicleListingRequestDto
    {
        [Required(ErrorMessage = "Car is required")]
        public Guid CarId { get; set; }

        [Required(ErrorMessage = "Mileage is required")]
        [Range(0, 999999, ErrorMessage = "Mileage must be between 0 and 999,999 miles")]
        public int Mileage { get; set; }

        [Required(ErrorMessage = "Color is required")]
        [MinLength(2, ErrorMessage = "Color must be at least 2 characters")]
        [MaxLength(30, ErrorMessage = "Color cannot exceed 30 characters")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Listing location is required")]
        public Guid listingLocationId { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(1, 10000000, ErrorMessage = "Price must be between $1 and $10,000,000")]
        public double Price { get; set; }

        [Required(ErrorMessage = "User is required")]
        public Guid UserId { get; set; }
    }
}
