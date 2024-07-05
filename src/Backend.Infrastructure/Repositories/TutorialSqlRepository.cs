namespace Backend.Infrastructure.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Backend.Core.Models;
    using Backend.Core.Repositories;
    using Backend.Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;

    public class TutorialSqlRepository : ITutorialRepository
    {
        private readonly MirasDbContext _context;

        public TutorialSqlRepository(MirasDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Tutorial> GetAll()
        {
            return _context.Tutorials.Where(tutorial => tutorial.IsActive == true);
        }

        public async Task<Tutorial?> GetByIdAsync(int id)
        {
            return await _context.Tutorials.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddAsync(Tutorial tutorial)
        {
            await _context.Tutorials.AddAsync(tutorial);
            
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var tutorial = await _context.Tutorials.FirstOrDefaultAsync(t => t.Id == id);
            if (tutorial != null)
            {
                tutorial.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(int id, Tutorial newTutorial)
        {
            var tutorial = await _context.Tutorials.FirstOrDefaultAsync(t => t.Id == id);
            if (tutorial != null)
            {
                tutorial.Title = newTutorial.Title;
                tutorial.VideoUrl = newTutorial.VideoUrl;
                tutorial.IsActive = newTutorial.IsActive;
                tutorial.Category = newTutorial.Category;
                tutorial.VideoDuration = newTutorial.VideoDuration;
                await _context.SaveChangesAsync();
            }
        }

        public IEnumerable<Tutorial> Search(string searchTerm)
        {
            return _context.Tutorials.Where(t => (t.Title.ToLower() ?? string.Empty).Contains(searchTerm.ToLower())
            || searchTerm.Contains(t.Title.ToLower() ?? string.Empty));
        }

        public IEnumerable<Tutorial> GetByCategory(string category)
        {
            return _context.Tutorials
                .Where(t => t.Category == category)
                .ToList();
        }
    }
}
