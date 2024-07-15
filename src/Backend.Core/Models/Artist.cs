namespace Backend.Core.Models;

public class Artist
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Major { get; set; }
    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public string? PhoneNumber { get; set; }
    public string? InstagramUrl { get; set; }
    public string? FacebookUrl { get; set; }
}