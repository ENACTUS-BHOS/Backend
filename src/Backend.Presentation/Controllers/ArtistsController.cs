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

    public ArtistsController(IArtistsService artistsService)
    {
        this.artistsService = artistsService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var artist = this.artistsService.GetAll();

        return base.Ok(artist);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(Artist? artist)
    {
        await this.artistsService.AddAsync(artist);

        return base.Created(base.HttpContext.Request.GetDisplayUrl(), artist);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(int? id)
    {
        await this.artistsService.RemoveAsync(id);

        return base.Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(int? id, Artist? artist)
    {
        await this.artistsService.UpdateAsync(id, artist);

        return base.Ok();
    }
}