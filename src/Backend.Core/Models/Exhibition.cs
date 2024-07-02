namespace Backend.Core.Models
{
    public class Exhibition
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
