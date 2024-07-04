namespace Backend.Presentation.Controllers;

using Backend.Core.Models;
using Backend.Core.Services;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/[controller]/[action]")]
public class OrdersController : ControllerBase
{
    private readonly IOrdersService ordersService;

    public OrdersController(IOrdersService ordersService) => this.ordersService = ordersService;

    [HttpGet]
    public IActionResult GetAll()
    {
        var orders = this.ordersService.GetAll();

        return base.Ok(orders);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetByIdAsync(int? id)
    {
        var orders = await this.ordersService.GetByIdAsync(id);

        return base.Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(Order? order)
    {
        await this.ordersService.AddAsync(order);

        return base.Created(base.HttpContext.Request.GetDisplayUrl(), order);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteAsync(int? id)
    {
        await this.ordersService.RemoveAsync(id);

        return base.Ok();
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateAsync(int? id, Order? order)
    {
        await this.ordersService.UpdateAsync(id, order);

        return base.Ok();
    }
}