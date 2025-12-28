using System.ComponentModel.DataAnnotations;

namespace secondcharge.api.Models.DTO.Location
{
    public class UpdateLocationRequestDto
    {
        [Required(ErrorMessage = "Country is required")]
        [MinLength(2, ErrorMessage = "Country must be at least 2 characters")]
        [MaxLength(50, ErrorMessage = "Country cannot exceed 50 characters")]
        public string Country { get; set; }

        [Required(ErrorMessage = "State is required")]
        [MinLength(2, ErrorMessage = "State must be at least 2 characters")]
        [MaxLength(50, ErrorMessage = "State cannot exceed 50 characters")]
        public string State { get; set; }

        [Required(ErrorMessage = "ZIP code is required")]
        [RegularExpression(@"^\d{5}(-\d{4})?$|^[A-Z]\d[A-Z] \d[A-Z]\d$|^\d{2,6}$", 
            ErrorMessage = "Invalid ZIP code format. Supported formats: US (12345 or 12345-6789), CA (A1A 1A1), or general (2-6 digits)")]
        public string zipCode { get; set; }
    }
}
