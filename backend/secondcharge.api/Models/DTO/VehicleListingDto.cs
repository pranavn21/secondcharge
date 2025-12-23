using secondcharge.api.Models.Domain;

namespace secondcharge.api.Models.DTO
{
    public class VehicleListingDto
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
        public Car CarModel { get; set; }
        public int Mileage { get; set; }
        public string Color { get; set; }
        public Location listingLocation { get; set; }
        public double Price { get; set; }
    }
}
