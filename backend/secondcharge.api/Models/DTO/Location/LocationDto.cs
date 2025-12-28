using System.ComponentModel.DataAnnotations;

namespace secondcharge.api.Models.DTO.Location
{
    public class LocationDto
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Country { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string State { get; set; }

        [Required]
        public string zipCode { get; set; }
    }
}
