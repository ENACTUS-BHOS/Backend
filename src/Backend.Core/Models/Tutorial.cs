namespace Backend.Core.Models
{
    public class Tutorial
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? TitleEn { get; set; }
        public string? VideoUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Category { get; set; }
        public string? CategoryEn { get; set; }
        public int? DurationInSeconds { get; set; }
    }
}
