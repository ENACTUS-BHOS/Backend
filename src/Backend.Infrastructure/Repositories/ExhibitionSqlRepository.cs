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
            return _context.Exhibitions.ToList();
        }

        public async Task<Exhibition?> GetByIdAsync(int id)
        {
            return await _context.Exhibitions.FindAsync(id);
        }

        public async Task AddAsync(Exhibition exhibition)
        {
            await _context.Exhibitions.AddAsync(exhibition);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var exhibition = await _context.Exhibitions.FindAsync(id);
            if (exhibition != null)
            {
                _context.Exhibitions.Remove(exhibition);
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

            return _context.Exhibitions
                .Where(e => e.Name !=null && e.Name.Contains(searchTerm) || (e.Description != null && e.Description.Contains(searchTerm)))
                .ToList();
        }
    }
}
