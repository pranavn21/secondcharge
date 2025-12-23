using secondcharge.api.Models.Domain;

namespace secondcharge.api.Models.DTO
{
    public class UpdateUserRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid userLocationId { get; set; }
        public string UserRole { get; set; }
        public string UserPhoneNumber { get; set; }
    }
}
