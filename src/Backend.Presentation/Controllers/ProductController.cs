namespace Backend.Presentation.Controllers;

using Backend.Core.Dtos;
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
    [Route("/api/[controller]/[action]/{id}")]
    public async Task<IActionResult> GetByIdAsync(int? id)
    {
        var product = await this.productsService.GetByIdAsync(id);

        return base.Ok(product);
    }

    [HttpGet]
    public IActionResult TakeTop(int? count)
    {
        var products = this.productsService.TakeTop(count);

        return base.Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Product? product)
    {
        await this.productsService.AddAsync(product);

        return base.Created(base.HttpContext.Request.GetDisplayUrl(), product);
    }

    [HttpDelete]
    [Route("/api/[controller]/[action]/{id}")]
    public async Task<IActionResult> DeleteAsync(int? id)
    {
        await this.productsService.RemoveAsync(id);

        return base.Ok();
    }

    [HttpPut]
    [Route("/api/[controller]/[action]/{id}")]
    public async Task<IActionResult> UpdateAsync(int? id, Product? product)
    {
        await this.productsService.UpdateAsync(id, product);

        return base.Ok();
    }

    [HttpPost]
    public async Task<IActionResult> OrderAsync(Product? product, UserDto userDto)
    {
        await this.productsService.OrderAsync(product, userDto);

        return base.Ok();
    }
}