namespace secondcharge.api.Models.DTO.User
{
    public class AddUserRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid LocationId { get; set; }
        public string UserRole { get; set; }
        public string UserPhoneNumber { get; set; }
    }
}
