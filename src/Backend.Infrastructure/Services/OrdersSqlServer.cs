using Backend.Core.Models;
using Backend.Core.Repositories;
using Backend.Core.Services;

namespace Backend.Infrastructure.Services;

public class OrdersSqlServer : IOrdersService
{
    private readonly IOrdersRepository ordersRepository;

    public OrdersSqlServer(IOrdersRepository ordersRepository) => this.ordersRepository = ordersRepository;

    public async Task AddAsync(Order? order)
    {
        ArgumentNullException.ThrowIfNull(order);

        await this.ordersRepository.AddAsync(order);
    }

    public IEnumerable<Order> GetAll()
    {
        var orders = this.ordersRepository.GetAll();

        return orders;
    }

    public Task<Order> GetByIdAsync(int? id)
    {
        ArgumentNullException.ThrowIfNull(id);

        var order = this.ordersRepository.GetByIdAsync((int)id);

        return order;
    }

    public async Task RemoveAsync(int? id)
    {
        ArgumentNullException.ThrowIfNull(id);

        await this.ordersRepository.RemoveAsync((int)id);
    }

    public async Task UpdateAsync(int? id, Order? newOrder)
    {
        ArgumentNullException.ThrowIfNull(id);

        ArgumentNullException.ThrowIfNull(newOrder);

        await this.ordersRepository.UpdateAsync((int)id, newOrder);
    }
}