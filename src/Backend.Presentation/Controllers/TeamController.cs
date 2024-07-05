namespace Backend.Presentation.Controllers;

using Backend.Core.Models;
using Backend.Core.Services;
using Backend.Infrastructure.Services;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("/api/[controller]/[action]")]
public class TeamController : ControllerBase
{
    private readonly ITeamService _teamService;
    private readonly BlobContainerService blobContainerService;

    public TeamController(ITeamService teamService)
    {
        _teamService = teamService;

        this.blobContainerService = new BlobContainerService();
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var teams = _teamService.GetAllTeams();
        return Ok(teams);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromForm] Team? team, IFormFile file)
    {
        if (team == null)
        {
            return BadRequest();
        }

        if (file != null)
        {
            var rawPath = Guid.NewGuid().ToString() + file.FileName;

            var path = rawPath.Replace(" ", "%20");

            team!.ImageUrl = "https://miras.blob.core.windows.net/multimedia/" + path;

            await this.blobContainerService.UploadAsync(file.OpenReadStream(), rawPath);
        }

        await _teamService.AddAsync(team);

        return Created(HttpContext.Request.GetDisplayUrl(), team);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteAsync(int? id)
    {
        if (!id.HasValue)
        {
            return BadRequest();
        }

        await _teamService.RemoveAsync(id);

        return Ok();
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateAsync(int? id, Team? team)
    {
        if (!id.HasValue || team == null || id != team.Id)
        {
            return BadRequest();
        }

        await _teamService.UpdateAsync(id, team);

        return Ok();
    }
}
