namespace Backend.Core.Models;

public class Team
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Role { get; set; }
    public string? Bio { get; set; }
    public string? NameEn { get; set; }
    public string? RoleEn { get; set; }
    public string? BioEn { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; } = true;
}
