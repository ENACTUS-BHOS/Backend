namespace Backend.Core.Services;

using Backend.Core.Dtos;
using Backend.Core.Models;

public interface IProductsService
{
    IEnumerable<Product> GetAll();
    Task<Product?> GetByIdAsync(int? id);
    IEnumerable<Product> TakeTop(int? count);
    Task AddAsync(Product? product);
    Task RemoveAsync(int? id);
    Task UpdateAsync(int? id, Product? newProduct);
    Task OrderAsync(Product? product, UserDto? userDto);
}