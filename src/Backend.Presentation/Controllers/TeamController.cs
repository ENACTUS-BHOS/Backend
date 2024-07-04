namespace Backend.Presentation.Controllers;

using Backend.Core.Models;
using Backend.Core.Services;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("/api/[controller]/[action]")]
public class TeamController : ControllerBase
{
    private readonly ITeamService _teamService;

    public TeamController(ITeamService teamService)
    {
        _teamService = teamService;
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
