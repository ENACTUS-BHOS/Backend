namespace Backend.Infrastructure.Repositories;

using Backend.Core.Dtos;
using Backend.Core.Models;
using Backend.Core.Repositories;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class ProductsSqlRepository : IProductsRepository
{
    private readonly MirasDbContext dbContext;

    public ProductsSqlRepository(MirasDbContext dbContext) => this.dbContext = dbContext;

    public IEnumerable<Product> GetAll()
    {
        var products = this.dbContext.Products.Where(product => product.IsActive == true);

        return products;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        var product = await this.dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

        return product;
    }

    public IEnumerable<Product> TakeTop(int count)
    {
        var products = this.dbContext.Products.Take(count);

        return products.AsEnumerable();
    }

    public async Task AddAsync(Product product)
    {
        await this.dbContext.Products.AddAsync(product);

        await this.dbContext.SaveChangesAsync();
    }

    public Task OrderAsync(Product product, UserDto userDto)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveAsync(int id)
    {
        var product = await this.dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

        product!.IsActive = false;

        await this.dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, Product newProduct)
    {
        var product = await this.dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

        product!.Name = newProduct.Name;
        
        product!.ArtistId = newProduct.ArtistId;
        
        product!.ImageUrl = newProduct.ImageUrl;
        
        product!.Price = newProduct.Price;
        
        product!.IsActive = newProduct.IsActive;

        await this.dbContext.SaveChangesAsync();
    }
}