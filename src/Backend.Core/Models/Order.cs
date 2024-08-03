namespace Backend.Core.Models;

public class Order
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? AdditionalInformation { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public bool IsActive { get; set; } = true;
}