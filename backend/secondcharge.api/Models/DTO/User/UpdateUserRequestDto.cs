using System;
using System.ComponentModel.DataAnnotations;

namespace secondcharge.api.Models.DTO.User
{
    public class UpdateUserRequestDto
    {
        [Required(ErrorMessage = "Username is required")]
        [MinLength(3, ErrorMessage = "Username must be at least 3 characters")]
        [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        [MaxLength(100, ErrorMessage = "Password cannot exceed 100 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public Guid LocationId { get; set; }

        [Required(ErrorMessage = "User role is required")]
        [RegularExpression(@"^(Admin|Buyer|Seller)$", 
            ErrorMessage = "User role must be Admin, Buyer, or Seller")]
        public string UserRole { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", 
            ErrorMessage = "Phone number must be in format: 555-123-4567")]
        public string UserPhoneNumber { get; set; }
    }
}
