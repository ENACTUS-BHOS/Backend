using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ExhibitionsController : ControllerBase
{
    private readonly ExhibitionContext _context;

    public ExhibitionsController(ExhibitionContext context)
    {
        _context = context;
    }

    // GET: api/Exhibitions
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Exhibition>>> GetExhibitions()
    {
        return await _context.Exhibitions.ToListAsync();
    }

    // GET: api/Exhibitions/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Exhibition>> GetExhibition(int id)
    {
        var exhibition = await _context.Exhibitions.FindAsync(id);

        if (exhibition == null)
        {
            return NotFound();
        }

        return exhibition;
    }

    // GET: api/Exhibitions/search?query=example
    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Exhibition>>> SearchExhibitions(string query)
    {
        if (string.IsNullOrEmpty(query))
        {
            return BadRequest("Query parameter is required.");
        }

        query = query.ToLower();

        var exhibitions = await _context.Exhibitions
            .Where(e => e.Title.ToLower().Contains(query) || e.Description.ToLower().Contains(query))
            .ToListAsync();

        return exhibitions;
    }
}
