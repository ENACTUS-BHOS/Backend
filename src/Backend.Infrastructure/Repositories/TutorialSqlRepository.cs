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
            return _context.Tutorials.AsNoTracking().ToList();
        }

        public async Task<Tutorial?> GetByIdAsync(int id)
        {
            return await _context.Tutorials.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddAsync(Tutorial tutorial)
        {
            _context.Tutorials.Add(tutorial);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var tutorial = await _context.Tutorials.FindAsync(id);
            if (tutorial != null)
            {
                _context.Tutorials.Remove(tutorial);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(int id, Tutorial newTutorial)
        {
            var tutorial = await _context.Tutorials.FindAsync(id);
            if (tutorial != null)
            {
                tutorial.Title = newTutorial.Title;
                tutorial.Description = newTutorial.Description;
                tutorial.VideoUrl = newTutorial.VideoUrl;
                tutorial.IsActive = newTutorial.IsActive;
                await _context.SaveChangesAsync();
            }
        }

        public IEnumerable<Tutorial> Search(string searchTerm)
        {
            return _context.Tutorials.AsNoTracking()
                                     .Where(t => (t.Title ?? string.Empty).Contains(searchTerm) || 
                                                 (t.Description ?? string.Empty).Contains(searchTerm))
                                     .ToList();
        }
    }
}
