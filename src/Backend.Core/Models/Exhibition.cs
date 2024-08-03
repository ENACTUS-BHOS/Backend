namespace Backend.Core.Models
{
    public class Exhibition
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? NameEn { get; set; }
        public string? DescriptionEn { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public List<string> ImageUrls { get; set; } = new List<string>(); // List of images for the detail page
    }
}
