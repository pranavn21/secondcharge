using secondcharge.api.Models.DTO.Location;

namespace secondcharge.api.Models.DTO.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid LocationId { get; set; }
        public string UserRole { get; set; }
        public string UserPhoneNumber { get; set; }
        public LocationDto? Location { get; set; }
    }
}
