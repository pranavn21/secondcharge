using Microsoft.AspNetCore.Components.Routing;

namespace secondcharge.api.Models.Domain
{
    public class VehicleListing
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
        public Car CarModel { get; set; }
        public int Mileage { get; set; }
        public string Color { get; set; }
        public Guid listingLocationId { get; set; }
        public double Price { get; set; }
        public Guid UserId { get; set; }

        // Navigational properties
        public User User { get; set; }
        public Location ListingLocation { get; set; }
    }
}
