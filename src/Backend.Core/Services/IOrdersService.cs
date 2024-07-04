namespace Backend.Core.Services;

using Backend.Core.Models;

public interface IOrdersService
{
    IEnumerable<Order> GetAll();
    Task<Order> GetByIdAsync(int? id);
    Task AddAsync(Order? order);
    Task RemoveAsync(int? id);
    Task UpdateAsync(int? id, Order? newOrder);
}