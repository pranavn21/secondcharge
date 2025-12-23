namespace secondcharge.api.Models.DTO
{
    public class UpdateCarRequestDto
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Efficiency { get; set; }
        public string? ModelImageUrl { get; set; }
    }
}
