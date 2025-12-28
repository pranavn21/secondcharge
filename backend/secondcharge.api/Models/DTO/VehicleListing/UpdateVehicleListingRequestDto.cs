using System.ComponentModel.DataAnnotations;

namespace secondcharge.api.Models.DTO.VehicleListing
{
    public class UpdateVehicleListingRequestDto
    {
        public Guid CarId { get; set; }

        [Required]
        public int Mileage { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public Guid listingLocationId { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
