using System.ComponentModel.DataAnnotations;
using secondcharge.api.Models.DTO.Car;
using secondcharge.api.Models.DTO.Location;
using secondcharge.api.Models.DTO.User;

namespace secondcharge.api.Models.DTO.VehicleListing
{
    public class VehicleListingDto
    {
        public Guid Id { get; set; }

        [Required]
        public Guid CarId { get; set; }

        [Required]
        [Range(0, 999999)]
        public int Mileage { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string Color { get; set; }

        [Required]
        public Guid listingLocationId { get; set; }

        [Required]
        [Range(1, 10000000)]
        public double Price { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public CarDto? CarModel { get; set; }
        public LocationDto? ListingLocation { get; set; }
        public UserDto? User { get; set; }
    }
}
