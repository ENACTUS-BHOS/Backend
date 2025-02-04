namespace Backend.Presentation.Controllers
{
    using Backend.Core.Models;
    using Backend.Core.Services;
    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class TutorialController : ControllerBase
    {
        private readonly ITutorialService _tutorialService;

        public TutorialController(ITutorialService tutorialService)
        {
            _tutorialService = tutorialService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var tutorials = _tutorialService.GetAllTutorials();
            return Ok(tutorials);
        }

        [HttpGet]
        public IActionResult Get(int skip, int take, string? search, string? category, bool? isSortAscending)
        {
            var tutorials = this._tutorialService.Get(skip, take, search, category, isSortAscending);

            return Ok(tutorials);
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categories = this._tutorialService.GetAllCategories();

            return base.Ok(categories);
        }

        [HttpGet]
        public IActionResult GetTutorialsCount(string? search, string? category, bool? isSortAscending)
        {
            var count = _tutorialService.TutorialsCount(search, category, isSortAscending);

            return base.Ok(count);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tutorial = await _tutorialService.GetByIdAsync(id);
            if (tutorial == null)
            {
                return NotFound();
            }
            return Ok(tutorial);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromForm]Tutorial tutorial)
        {
            if (tutorial == null)
            {
                return BadRequest();
            }

            await _tutorialService.AddAsync(tutorial);

            return Created(HttpContext.Request.GetDisplayUrl(), tutorial);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _tutorialService.RemoveAsync(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Tutorial tutorial)
        {
            if (tutorial == null)
            {
                return BadRequest();
            }
            
            await _tutorialService.UpdateAsync(id, tutorial);

            return Ok();
        }

        [HttpGet]
        public IActionResult Search(string searchTerm)
        {
            var results = _tutorialService.Search(searchTerm);
            return Ok(results);
        }

        [HttpGet("{category}")]
        public IActionResult GetByCategory(string category)
        {
            var tutorials = _tutorialService.GetByCategory(category);
            return Ok(tutorials);
        }
    }
}
