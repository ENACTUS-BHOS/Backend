namespace Backend.Core.Models;

public class ProductsCount
{
    public IEnumerable<Artist> Artists { get; set; }
    
    public int Count { get; set; }
}