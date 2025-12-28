namespace secondcharge.api.Models.DTO.User
{
    public class UpdateUserRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid LocationId { get; set; }
        public string UserRole { get; set; }
        public string UserPhoneNumber { get; set; }
    }
}
