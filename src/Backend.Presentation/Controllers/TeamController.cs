// namespace Backend.Presentation.Controllers
// {
//     using Microsoft.AspNetCore.Mvc;
//     using Backend.Core.Models;
//     using Backend.Core.Services;
//     using System.Collections.Generic;
//     using System.Threading.Tasks;

//     [ApiController]
//     [Route("api/[controller]")]
//     public class TeamController : ControllerBase
//     {
//         private readonly ITeamService teamService;

//         public TeamController(ITeamService teamService)
//         {
//             this.teamService = teamService;
//         }

//         [HttpGet]
//         public IActionResult GetAll()
//         {
//             var teams = teamService.GetAllTeams();
//             return Ok(teams);
//         }

//         [HttpGet("{id}")]
//         public async Task<IActionResult> GetById(int id)
//         {
//             var team = await teamService.GetByIdAsync(id);
//             if (team == null)
//             {
//                 return NotFound();
//             }
//             return Ok(team);
//         }

//         [HttpPost]
//         public async Task<IActionResult> Create(Team team)
//         {
//             await teamService.AddAsync(team);
//             return CreatedAtAction(nameof(GetById), new { id = team.Id }, team);
//         }

//         [HttpPut("{id}")]
//         public async Task<IActionResult> Update(int id, Team team)
//         {
//             if (id != team.Id)
//             {
//                 return BadRequest();
//             }

//             await teamService.UpdateAsync(id, team);
//             return NoContent();
//         }

//         [HttpDelete("{id}")]
//         public async Task<IActionResult> Delete(int id)
//         {
//             await teamService.RemoveAsync(id);
//             return NoContent();
//         }
//     }
// }
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
    public async Task<IActionResult> AddAsync(Team? team)
    {
        if (team == null)
        {
            return BadRequest();
        }

        await _teamService.CreateTeam(team);
        return Created(HttpContext.Request.GetDisplayUrl(), team);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(int? id)
    {
        if (!id.HasValue)
        {
            return BadRequest();
        }

        await _teamService.DeleteTeam(id.Value);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(int? id, Team? team)
    {
        if (!id.HasValue || team == null || id != team.Id)
        {
            return BadRequest();
        }

        await _teamService.UpdateTeam(team);
        return Ok();
    }
}
