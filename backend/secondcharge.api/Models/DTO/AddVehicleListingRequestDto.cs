using System.ComponentModel.DataAnnotations;
using secondcharge.api.Models.Domain;

namespace secondcharge.api.Models.DTO
{
    public class AddVehicleListingRequestDto
    {
        [Required]
        public Guid CarId { get; set; }

        [Required]
        public int Mileage { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public Guid listingLocationId { get; set; }

        [Required]
        public double Price { get; set; }
    }
}
