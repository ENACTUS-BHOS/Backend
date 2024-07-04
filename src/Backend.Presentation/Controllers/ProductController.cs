namespace Backend.Presentation.Controllers;

using Backend.Core.Models;
using Backend.Core.Services;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/[controller]/[action]")]
public class ProductController : ControllerBase
{
    private readonly IProductsService productsService;

    public ProductController(IProductsService productsService) => this.productsService = productsService;

    [HttpGet]
    public IActionResult GetAll()
    {
        var products = this.productsService.GetAll();

        return base.Ok(products);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetByIdAsync(int? id)
    {
        var product = await this.productsService.GetByIdAsync(id);

        return base.Ok(product);
    }

    [HttpGet]
    [Route("{authorId}")]
    public IActionResult GetByAuthorId(int? authorId)
    {
        var product = this.productsService.GetByAuthorId(authorId);

        return base.Ok(product);
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
}