using secondcharge.api.Models.DTO.Car;
using secondcharge.api.Models.DTO.Location;
using secondcharge.api.Models.DTO.User;

namespace secondcharge.api.Models.DTO.VehicleListing
{
    public class VehicleListingDto
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
        public int Mileage { get; set; }
        public string Color { get; set; }
        public Guid listingLocationId { get; set; }
        public double Price { get; set; }
        public Guid UserId { get; set; }
        public CarDto? CarModel { get; set; }
        public LocationDto? ListingLocation { get; set; }
        public UserDto? User { get; set; }
    }
}
