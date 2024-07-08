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

        public IEnumerable<Tutorial> Get(int skip, int take)
        {
            var tutorials = this._context.Tutorials.Skip(skip).Take(take);

            return tutorials;
        }

        public IEnumerable<string> GetAllCategories()
        {
            var categories = this._context.Tutorials.Select(tutorial => tutorial.Category).Distinct();

            return categories!;
        }

        public IEnumerable<Tutorial> Get(int skip, int take, string? search, string? category, bool? isSortAscending)
        {
            var tutorials = GetAll();

            if (!string.IsNullOrWhiteSpace(search))
            {
                tutorials = Search(search);
            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                tutorials = tutorials.Where(tutorial => tutorial.Category!.ToLower().Contains(category.ToLower()) || category.ToLower().Contains(tutorial.Category.ToLower()));
            }

            if (isSortAscending != null)
            {
                if (isSortAscending == true)
                {
                    tutorials = tutorials.OrderBy(tutorial => tutorial.DurationInSeconds);
                }
                else
                {
                    tutorials = tutorials.OrderByDescending(tutorial => tutorial.DurationInSeconds);
                }
            }

            tutorials = tutorials.Skip(skip).Take(take);

            return tutorials;
        }

        public int TutorialsCount(string? search, string? category, bool? isSortAscending)
        {
            var tutorials = GetAll();

            if (!string.IsNullOrWhiteSpace(search))
            {
                tutorials = Search(search);
            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                tutorials = tutorials.Where(tutorial => tutorial.Category!.ToLower().Contains(category.ToLower()) || category.ToLower().Contains(tutorial.Category.ToLower()));
            }

            if (isSortAscending != null)
            {
                if (isSortAscending == true)
                {
                    tutorials = tutorials.OrderBy(tutorial => tutorial.DurationInSeconds);
                }
                else
                {
                    tutorials = tutorials.OrderByDescending(tutorial => tutorial.DurationInSeconds);
                }
            }

            var count = tutorials.Count();

            return count;
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
                tutorial.DurationInSeconds = newTutorial.DurationInSeconds;
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
