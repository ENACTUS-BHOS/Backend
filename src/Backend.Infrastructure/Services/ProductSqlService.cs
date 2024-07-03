using Backend.Core.Dtos;
using Backend.Core.Models;
using Backend.Core.Repositories;
using Backend.Core.Services;

namespace Backend.Infrastructure.Services;

public class ProductSqlService : IProductsService
{
    private readonly IProductsRepository productsRepository;

    public ProductSqlService(IProductsRepository productsRepository) => this.productsRepository = productsRepository;

    public async Task AddAsync(Product? product)
    {
        ArgumentNullException.ThrowIfNull(product);

        await this.productsRepository.AddAsync(product);
    }

    public IEnumerable<Product> GetAll()
    {
        var products = this.productsRepository.GetAll();

        return products;
    }

    public Task<Product?> GetByIdAsync(int? id)
    {
        ArgumentNullException.ThrowIfNull(id);

        var product = this.productsRepository.GetByIdAsync((int)id);

        return product;
    }

    public async Task OrderAsync(Product? product, UserDto? userDto)
    {
        ArgumentNullException.ThrowIfNull(product);

        ArgumentNullException.ThrowIfNull(userDto);

        await this.productsRepository.OrderAsync(product, userDto);
    }

    public async Task RemoveAsync(int? id)
    {
        ArgumentNullException.ThrowIfNull(id);

        await this.productsRepository.RemoveAsync((int)id);
    }

    public IEnumerable<Product> TakeTop(int? count)
    {
        ArgumentNullException.ThrowIfNull(count);
        
        var products = this.productsRepository.TakeTop((int)count);

        return products;
    }

    public async Task UpdateAsync(int? id, Product? newProduct)
    {
        ArgumentNullException.ThrowIfNull(id);
        
        ArgumentNullException.ThrowIfNull(newProduct);

        await this.productsRepository.UpdateAsync((int)id, newProduct);
    }
}