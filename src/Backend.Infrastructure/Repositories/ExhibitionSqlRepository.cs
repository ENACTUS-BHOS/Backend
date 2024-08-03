namespace Backend.Infrastructure.Repositories
{
    using Backend.Core.Models;
    using Backend.Core.Repositories;
    using Backend.Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ExhibitionSqlRepository : IExhibitionRepository
    {
        private readonly MirasDbContext _context;

        public ExhibitionSqlRepository(MirasDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Exhibition> GetAll()
        {
            return _context.Exhibitions
                .AsNoTracking()
                .Where(e => e.IsActive == true)
                .OrderByDescending(e => e.Id)
                .ToList()
                .DistinctBy(e => e.Id);
        }

        public async Task<Exhibition?> GetByIdAsync(int id)
        {
            return await _context.Exhibitions.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Exhibition>> GetAsync(int skip, int take, string search)
        {
            var allExhibitions = GetAll();

            if (!string.IsNullOrWhiteSpace(search))
            {
                allExhibitions = allExhibitions.Where(t => (t.Name.ToLower() ?? string.Empty).Contains(search.ToLower()) || (t.Description.ToLower() ?? string.Empty).Contains(search.ToLower())
                    || search.Contains(t.Name.ToLower() ?? string.Empty) || search.Contains(t.Description.ToLower() ?? string.Empty));
            }

            allExhibitions = allExhibitions.Skip(skip).Take(take);

            return allExhibitions;
        }

        public async Task AddAsync(Exhibition exhibition)
        {
            await _context.Exhibitions.AddAsync(exhibition);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var exhibition = await _context.Exhibitions.FirstOrDefaultAsync(e => e.Id == id);
            if (exhibition != null)
            {
                exhibition.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(int id, Exhibition newExhibition)
        {
            var existingExhibition = await _context.Exhibitions.FindAsync(id);
            if (existingExhibition != null)
            {
                existingExhibition.Name = newExhibition.Name;
                existingExhibition.Description = newExhibition.Description; // Handle possible null
                existingExhibition.ImageUrl = newExhibition.ImageUrl;
                existingExhibition.VideoUrl = newExhibition.VideoUrl;
                existingExhibition.ImageUrls = newExhibition.ImageUrls; // Update the list of images
                existingExhibition.IsActive = newExhibition.IsActive;

                _context.Exhibitions.Update(existingExhibition);
                await _context.SaveChangesAsync();
            }
        }

        public IEnumerable<Exhibition> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return Enumerable.Empty<Exhibition>();
            }

            return _context.Exhibitions.Where(t => (t.Name.ToLower() ?? string.Empty).Contains(searchTerm.ToLower()) || (t.Description.ToLower() ?? string.Empty).Contains(searchTerm.ToLower())
            || searchTerm.Contains(t.Name.ToLower() ?? string.Empty) || searchTerm.Contains(t.Description.ToLower() ?? string.Empty));
        }
    }
}
