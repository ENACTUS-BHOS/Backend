namespace Backend.Presentation.Controllers;

using System.Linq;
using Backend.Core.Models;
using Backend.Core.Services;
using Backend.Infrastructure.Services;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/[controller]/[action]")]
public class ProductController : ControllerBase
{
    private readonly IProductsService productsService;
    private readonly BlobContainerService blobContainerService;

    public ProductController(IProductsService productsService)
    {
        this.productsService = productsService;

        this.blobContainerService = new BlobContainerService();
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var products = this.productsService.GetAll();

        return base.Ok(products);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int? id)
    {
        var product = await this.productsService.GetByIdAsync(id);

        return base.Ok(product);
    }

    [HttpGet]
    public IActionResult GetByAuthorId(int? authorId, int? skip, int? take, string? search, int? minimumPrice, int? maximumPrice, bool? isSortAscending)
    {
        var products = this.productsService.GetByAuthorId(authorId);

        if (!string.IsNullOrWhiteSpace(search))
        {
            products = products.Where(p => p.Name.ToLower().Contains(search.ToLower()) || search.ToLower().Contains(p.Name.ToLower()));
        }

        if (minimumPrice != null)
        {
            products = products.Where(p => p.Price > minimumPrice);
        }

        if (maximumPrice != null)
        {
            products = products.Where(p => p.Price < maximumPrice);
        }

        if (isSortAscending != null)
        {
            if (isSortAscending == true)
            {
                products = products.OrderBy(p => p.Price);
            }
            else
            {
                products = products.OrderByDescending(p => p.Price);
            }
        }

        if (skip != null && take != null)
        {
            products = products.Skip((int)skip).Take((int)take);
        }

        return base.Ok(products);
    }

    [HttpGet]
    public IActionResult GetProductsCount(int? authorId, string? search, int? minimumPrice, int? maximumPrice, bool? isSortAscending)
    {
        var products = this.productsService.GetByAuthorId(authorId);

        if (!string.IsNullOrWhiteSpace(search))
        {
            products = products.Where(p => p.Name.ToLower().Contains(search.ToLower()) || search.ToLower().Contains(p.Name.ToLower()));
        }

        if (minimumPrice != null)
        {
            products = products.Where(p => p.Price > minimumPrice);
        }

        if (maximumPrice != null)
        {
            products = products.Where(p => p.Price < maximumPrice);
        }

        if (isSortAscending != null)
        {
            if (isSortAscending == true)
            {
                products = products.OrderBy(p => p.Price);
            }
            else
            {
                products = products.OrderByDescending(p => p.Price);
            }
        }

        return base.Ok(products.Count());
    }

    [HttpGet]
    [Route("{count}")]
    public IActionResult TakeTop(int? count)
    {
        var products = this.productsService.TakeTop(count);

        return base.Ok(products);
    }

    [HttpGet]
    [Route("{authorId}/{isAscending}")]
    public IActionResult GetSortedByPrice(int? authorId, bool? isAscending)
    {
        var products = this.productsService.SortByPrice(authorId, isAscending);

        return base.Ok(products);
    }

    [HttpGet]
    [Route("{authorId}/{minimumPrice}/{maximumPrice}")]
    public IActionResult GetFilteredByPrice(int? authorId, int? minimumPrice, int? maximumPrice)
    {
        var products = this.productsService.FilterByPrice(authorId, minimumPrice, maximumPrice);

        return base.Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromForm] Product? product, IFormFile file)
    {
        if (file != null)
        {
            var rawPath = Guid.NewGuid().ToString() + file.FileName;

            var path = rawPath.Replace(" ", "%20");

            product!.ImageUrl = "https://miras.blob.core.windows.net/multimedia/" + path;

            await this.blobContainerService.UploadAsync(file.OpenReadStream(), rawPath);
        }

        product.Artist = null;
        
        await this.productsService.AddAsync(product);

        return base.Created(base.HttpContext.Request.GetDisplayUrl(), product);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteAsync(int? id)
    {
        await this.productsService.RemoveAsync(id);

        return base.Ok();
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateAsync(int? id, Product? product)
    {
        await this.productsService.UpdateAsync(id, product);

        return base.Ok();
    }

    [HttpPost]
    public async Task<IActionResult> OrderAsync(Order? order)
    {
        await this.productsService.OrderAsync(order!);

        return base.Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Translate()
    {
        await this.productsService.Translate();

        return base.Ok();
    }
}