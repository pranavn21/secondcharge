using System.ComponentModel.DataAnnotations;

namespace secondcharge.api.Models.DTO.Login
{
    public class LoginRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
