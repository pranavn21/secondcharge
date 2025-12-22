namespace secondcharge.api.Models.Domain
{
    public class Location
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public int zipCode { get; set; }

    }
}
