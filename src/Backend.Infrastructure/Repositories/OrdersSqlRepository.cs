namespace Backend.Infrastructure.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Core.Models;
using Backend.Core.Repositories;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class OrdersSqlRepository : IOrdersRepository
{
    private readonly MirasDbContext dbContext;

    public OrdersSqlRepository(MirasDbContext dbContext) => this.dbContext = dbContext;

    public async Task AddAsync(Order order)
    {
        await this.dbContext.Orders.AddAsync(order);

        await this.dbContext.SaveChangesAsync();
    }

    public IEnumerable<Order> GetAll()
    {
        var orders = this.dbContext.Orders.Where(order => order.IsActive == true);

        return orders;
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        var order = await this.dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);

        return order!;
    }

    public async Task RemoveAsync(int id)
    {
        var order = await this.dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
    
        order!.IsActive = false;

        await this.dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, Order newOrder)
    {
        var order = await this.dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
    
        order!.FullName = newOrder!.FullName;
        
        order!.Email = newOrder!.Email;
        
        order!.PhoneNumber = newOrder!.PhoneNumber;
        
        order!.AdditionalInformation = newOrder!.AdditionalInformation;
        
        order!.ProductId = newOrder!.ProductId;
        
        order!.IsActive = newOrder!.IsActive;

        await this.dbContext.SaveChangesAsync();
    }
}