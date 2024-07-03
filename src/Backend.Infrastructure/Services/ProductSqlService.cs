using Backend.Core.Dtos;
using Backend.Core.Models;
using Backend.Core.Repositories;
using Backend.Core.Services;

namespace Backend.Infrastructure.Services;

public class ProductSqlService : IProductsService
{
    private readonly IProductsRepository productsRepository;

    public ProductSqlService(IProductsRepository productsRepository) => this.productsRepository = productsRepository;

    public async Task AddAsync(Product product)
    {
        await this.productsRepository.AddAsync(product);
    }

    public IEnumerable<Product> GetAll()
    {
        var products = this.productsRepository.GetAll();

        return products;
    }

    public Task<Product?> GetByIdAsync(int id)
    {
        var product = this.productsRepository.GetByIdAsync(id);

        return product;
    }

    public async Task OrderAsync(Product product, UserDto userDto)
    {
        await this.productsRepository.OrderAsync(product, userDto);
    }

    public async Task RemoveAsync(int id)
    {
        await this.productsRepository.RemoveAsync(id);
    }

    public IEnumerable<Product> TakeTop(int count)
    {
        var products = this.productsRepository.TakeTop(count);

        return products;
    }

    public async Task UpdateAsync(int id, Product newProduct)
    {
        await this.productsRepository.UpdateAsync(id, newProduct);
    }
}