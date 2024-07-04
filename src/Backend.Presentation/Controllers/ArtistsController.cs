namespace Backend.Presentation.Controllers;

using Backend.Core.Models;
using Backend.Core.Services;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/[controller]/[action]")]
public class ArtistsController : ControllerBase
{
    private readonly IArtistsService artistsService;

    public ArtistsController(IArtistsService artistsService) => this.artistsService = artistsService;

    [HttpGet]
    public IActionResult GetAll()
    {
        var artist = this.artistsService.GetAll();

        return base.Ok(artist);
    }

    [HttpGet]
    [Route("{skip}/{take}")]
    public IActionResult Get(int? skip, int? take)
    {
        var products = this.artistsService.Get(skip, take);

        return base.Ok(products);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetByIdAsync(int? id)
    {
        var product = await this.artistsService.GetByIdAsync(id);

        return base.Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromForm] Artist? artist, IFormFile file)
    {
        await this.artistsService.AddAsync(artist);

        return base.Created(base.HttpContext.Request.GetDisplayUrl(), artist);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteAsync(int? id)
    {
        await this.artistsService.RemoveAsync(id);

        return base.Ok();
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateAsync(int? id, Artist? artist)
    {
        await this.artistsService.UpdateAsync(id, artist);

        return base.Ok();
    }
}