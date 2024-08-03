namespace Backend.Presentation.Controllers
{
    using Backend.Core.Models;
    using Backend.Core.Services;
    using Backend.Infrastructure.Services;
    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class ExhibitionController : ControllerBase
    {
        private readonly BlobContainerService blobContainerService;
        private readonly IExhibitionService _exhibitionService;

        public ExhibitionController(IExhibitionService exhibitionService)
        {
            _exhibitionService = exhibitionService;

            this.blobContainerService = new BlobContainerService();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var exhibitions = _exhibitionService.GetAllExhibitions();
            
            return Ok(exhibitions);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(int skip, int take, string? search)
        {
            var exhibitions = await this._exhibitionService.GetAsync(skip, take, search);

            return base.Ok(exhibitions);
        }

        [HttpGet]
        public IActionResult GetCount(string? search)
        {
            var exhibitions = Enumerable.Empty<Exhibition>();
            
            if (!string.IsNullOrWhiteSpace(search))
            {
                exhibitions = this._exhibitionService.Search(search);
            }
            else
            {
                exhibitions = this._exhibitionService.GetAllExhibitions();
            }
            
            var count = exhibitions.Count();

            return base.Ok(count);
        }

        [HttpGet]
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
        public async Task<IActionResult> AddAsync([FromForm] Exhibition exhibition, IFormFile coverImage, IFormFile video, IFormFileCollection images)
        {
            if (exhibition == null)
            {
                return BadRequest();
            }

            if (coverImage != null)
            {
                var rawPath = Guid.NewGuid().ToString() + coverImage.FileName;

                var path = rawPath.Replace(" ", "%20");

                exhibition!.ImageUrl = "https://miras.blob.core.windows.net/multimedia/" + path;

                await this.blobContainerService.UploadAsync(coverImage.OpenReadStream(), rawPath);
            }

            if (video != null)
            {
                var rawPath = Guid.NewGuid().ToString() + video.FileName;

                var path = rawPath.Replace(" ", "%20");

                exhibition!.VideoUrl = "https://miras.blob.core.windows.net/multimedia/" + path;

                await this.blobContainerService.UploadAsync(video.OpenReadStream(), rawPath);
            }

            if (images != null)
            {
                foreach (var image in images)
                {
                    var rawPath = Guid.NewGuid().ToString() + image.FileName;

                    var path = rawPath.Replace(" ", "%20");

                    exhibition!.ImageUrls.Add("https://miras.blob.core.windows.net/multimedia/" + path);

                    await this.blobContainerService.UploadAsync(image.OpenReadStream(), rawPath);
                }
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
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] Exhibition exhibition)
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
