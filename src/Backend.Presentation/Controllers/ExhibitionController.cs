using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backend.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


[Route("api/[controller]")]
[ApiController]
public class ExhibitionController : ControllerBase
{
    private readonly IExhibitionService _service;

    public ExhibitionController(IExhibitionService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Exhibition>>> GetExhibitions()
    {
        var exhibitions = await _service.GetExhibitionsAsync();
        return Ok(exhibitions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Exhibition>> GetExhibition(int id)
    {
        var exhibition = await _service.GetExhibitionByIdAsync(id);
        if (exhibition == null)
        {
            return NotFound();
        }
        return Ok(exhibition);
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Exhibition>>> SearchExhibitions(string query)
    {
        if (string.IsNullOrEmpty(query))
        {
            return BadRequest("Query parameter is required.");
        }

        var exhibitions = await _service.SearchExhibitionsAsync(query.ToLower());
        return Ok(exhibitions);
    }

    [HttpPost]
    public async Task<ActionResult> AddExhibition(Exhibition exhibition)
    {
        await _service.AddExhibitionAsync(exhibition);
        return CreatedAtAction(nameof(GetExhibition), new { id = exhibition.Id }, exhibition);
    }
}
