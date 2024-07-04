namespace Backend.Presentation.Controllers
{
    using Backend.Core.Models;
    using Backend.Core.Services;
    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class ExhibitionController : ControllerBase
    {
        private readonly IExhibitionService _exhibitionService;

        public ExhibitionController(IExhibitionService exhibitionService)
        {
            _exhibitionService = exhibitionService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var exhibitions = _exhibitionService.GetAllExhibitions();
            return Ok(exhibitions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var exhibition = await _exhibitionService.GetByIdAsync(id);
            if (exhibition == null)
            {
                return NotFound();
            }
            return Ok(exhibition);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromForm] Exhibition exhibition, IFormFile file)
        {
            if (exhibition == null)
            {
                return BadRequest();
            }

            await _exhibitionService.AddAsync(exhibition);

            return Created(HttpContext.Request.GetDisplayUrl(), exhibition);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _exhibitionService.RemoveAsync(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Exhibition exhibition)
        {
            if (exhibition == null)
            {
                return BadRequest();
            }

            await _exhibitionService.UpdateAsync(id, exhibition);

            return Ok();
        }

        [HttpGet]
        public IActionResult Search(string searchTerm)
        {
            var results = _exhibitionService.Search(searchTerm);
            return Ok(results);
        }
    }
}
