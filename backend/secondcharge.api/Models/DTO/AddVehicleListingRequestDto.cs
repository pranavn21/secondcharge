using secondcharge.api.Models.Domain;

namespace secondcharge.api.Models.DTO
{
    public class AddVehicleListingRequestDto
    {
        public Car CarModel { get; set; }
        public int Mileage { get; set; }
        public string Color { get; set; }
        public Location listingLoction { get; set; }
        public double Price { get; set; }
    }
}
