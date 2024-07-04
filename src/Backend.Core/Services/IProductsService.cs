namespace Backend.Core.Services;

using Backend.Core.Models;

public interface IProductsService
{
    IEnumerable<Product> GetAll();
    IEnumerable<Product> GetByAuthorId(int? authorId);
    Task<Product?> GetByIdAsync(int? id);
    IEnumerable<Product> TakeTop(int? count);
    Task AddAsync(Product? product);
    Task RemoveAsync(int? id);
    Task UpdateAsync(int? id, Product? newProduct);
    Task OrderAsync(Order order);
    IEnumerable<Product> SortByPrice(int? authorId, bool? isAscending);
    IEnumerable<Product> FilterByPrice(int? authorId, int? minimumPrice, int? maximumPrice);
}