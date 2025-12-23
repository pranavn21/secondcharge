using secondcharge.api.Models.Domain;

namespace secondcharge.api.Models.DTO
{
    public class AddVehicleListingRequestDto
    {
        public Guid CarId { get; set; }
        public int Mileage { get; set; }
        public string Color { get; set; }
        public Guid listingLocationId { get; set; }
        public double Price { get; set; }
    }
}
