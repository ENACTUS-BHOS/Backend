namespace Backend.Core.Models;

public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int? Price { get; set; }
    public string? ImageUrl { get; set; }
    public int? ArtistId { get; set; }
    public bool IsActive { get; set; } = true;
}