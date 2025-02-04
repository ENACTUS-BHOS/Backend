namespace Backend.Infrastructure.Services;

using Backend.Core.Models;
using Backend.Core.Repositories;
using Backend.Core.Services;

public class ProductSqlService : IProductsService
{
    private readonly IProductsRepository productsRepository;

    public ProductSqlService(IProductsRepository productsRepository) => this.productsRepository = productsRepository;

    public async Task AddAsync(Product? product)
    {
        ArgumentNullException.ThrowIfNull(product);

        await this.productsRepository.AddAsync(product);
    }

    public IEnumerable<Product> FilterByPrice(int? authorId, int? minimumPrice, int? maximumPrice)
    {
        ArgumentNullException.ThrowIfNull(authorId);

        ArgumentNullException.ThrowIfNull(minimumPrice);
        
        ArgumentNullException.ThrowIfNull(maximumPrice);

        var products = this.productsRepository.FilterByPrice((int)authorId, (int)minimumPrice, (int)maximumPrice);

        return products;
    }

    public IEnumerable<Product> GetAll()
    {
        var products = this.productsRepository.GetAll();

        return products;
    }

    public IEnumerable<Product> GetByAuthorId(int? authorId)
    {
        ArgumentNullException.ThrowIfNull(authorId);

        var products = this.productsRepository.GetByAuthorId((int)authorId);

        return products;
    }

    public Task<Product?> GetByIdAsync(int? id)
    {
        ArgumentNullException.ThrowIfNull(id);

        var product = this.productsRepository.GetByIdAsync((int)id);

        return product;
    }

    public async Task OrderAsync(Order? order)
    {
        ArgumentNullException.ThrowIfNull(order);

        await this.productsRepository.OrderAsync(order);
    }

    public async Task RemoveAsync(int? id)
    {
        ArgumentNullException.ThrowIfNull(id);

        await this.productsRepository.RemoveAsync((int)id);
    }

    public IEnumerable<Product> SortByPrice(int? authorId, bool? isAscending)
    {
        ArgumentNullException.ThrowIfNull(authorId);

        ArgumentNullException.ThrowIfNull(isAscending);

        var sortedProducts = this.productsRepository.SortByPrice((int)authorId, (bool)isAscending);

        return sortedProducts;
    }

    public IEnumerable<Product> TakeTop(int? count)
    {
        ArgumentNullException.ThrowIfNull(count);
        
        var products = this.productsRepository.TakeTop((int)count);

        return products;
    }

    public async Task Translate()
    {
        await this.productsRepository.Translate();
    }

    public async Task UpdateAsync(int? id, Product? newProduct)
    {
        ArgumentNullException.ThrowIfNull(id);

        ArgumentNullException.ThrowIfNull(newProduct);

        await this.productsRepository.UpdateAsync((int)id, newProduct);
    }
}