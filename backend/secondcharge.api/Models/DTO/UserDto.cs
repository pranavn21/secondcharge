using secondcharge.api.Models.Domain;

namespace secondcharge.api.Models.DTO
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid userLocationId { get; set; }
        public string UserRole { get; set; }
        public string UserPhoneNumber { get; set; }
    }
}
