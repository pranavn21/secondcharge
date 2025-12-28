using System.ComponentModel.DataAnnotations;
using secondcharge.api.Models.DTO.Location;

namespace secondcharge.api.Models.DTO.User
{
    public class UserDto
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        public Guid LocationId { get; set; }

        [Required]
        [RegularExpression(@"^(Admin|Buyer|Seller)$")]
        public string UserRole { get; set; }

        [Required]
        [Phone]
        public string UserPhoneNumber { get; set; }

        public LocationDto? Location { get; set; }
    }
}
