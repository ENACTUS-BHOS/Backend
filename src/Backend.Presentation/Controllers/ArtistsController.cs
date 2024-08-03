namespace Backend.Presentation.Controllers;

using Backend.Core.Models;
using Backend.Core.Services;
using Backend.Infrastructure.Services;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/[controller]/[action]")]
public class ArtistsController : ControllerBase
{
    private readonly IArtistsService artistsService;
    private readonly BlobContainerService blobContainerService;

    public ArtistsController(IArtistsService artistsService)
    {
        this.artistsService = artistsService;

        this.blobContainerService = new BlobContainerService();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var artists = await this.artistsService.GetAll();

        return base.Ok(artists);
    }

    [HttpGet]
    public async Task<IActionResult> Get(int? skip, int? take, int? takeProducts, string? search, int? minimumPrice,
        int? maximumPrice, bool? isSortAscending)
    {
        var artistsCount = await this.artistsService.GetAsync(skip, take, takeProducts, search, minimumPrice, maximumPrice,
            isSortAscending);

        return base.Ok(artistsCount);
    }

    [HttpGet]
    public async Task<IActionResult> GetByIdAsync(int id, int skip, int take, string? search,
        int? minimumPrice, int? maximumPrice, bool? isSortAscending)
    {
        var product = await this.artistsService.GetByIdAsync(id, skip, take, search, minimumPrice, maximumPrice, isSortAscending);

        return base.Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromForm] Artist? artist, IFormFile? file)
    {
        if (file != null)
        {
            var rawPath = Guid.NewGuid().ToString() + file.FileName;

            var path = rawPath.Replace(" ", "%20");

            artist!.ImageUrl = "https://miras.blob.core.windows.net/multimedia/" + path;

            await this.blobContainerService.UploadAsync(file.OpenReadStream(), rawPath);
        }

        await this.artistsService.AddAsync(artist);

        return base.Created(base.HttpContext.Request.GetDisplayUrl(), artist);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int? id)
    {
        await this.artistsService.RemoveAsync(id);

        return base.Ok();
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int? id, Artist? artist)
    {
        await this.artistsService.UpdateAsync(id, artist);

        return base.Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Translate()
    {
        await this.artistsService.Translate();

        return base.Ok();
    }
}