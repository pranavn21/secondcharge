namespace secondcharge.api.Models.Domain
{
    public class Car
    {
        public Guid Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Efficiency { get; set; }
        public string? ModelImageUrl { get; set; }
    }
}
