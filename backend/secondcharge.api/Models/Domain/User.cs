namespace secondcharge.api.Models.Domain
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; } // do salted hash later?
        public Guid Id { get; set; }
        public string UserRole { get; set; }
        public int UserPhoneNumber { get; set; }
    }
}
