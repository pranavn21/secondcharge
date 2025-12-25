namespace secondcharge.api.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; } // do salted hash later?
        public Guid LocationId { get; set; }
        public string UserRole { get; set; }
        public string UserPhoneNumber { get; set; }

        // Navigation properties
        public Location? Location { get; set; }

    }
}
