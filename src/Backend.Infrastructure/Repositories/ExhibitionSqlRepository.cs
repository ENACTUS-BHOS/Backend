using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Core.Models;
using Backend.Core.Repositories;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Repositories
{
    public class ExhibitionRepository : IExhibitionRepository
    {
        private readonly ExhibitionContext _context;

        public ExhibitionRepository(ExhibitionContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Exhibition>> GetExhibitionsAsync()
        {
            return await _context.Exhibitions.ToListAsync();
        }

        public async Task<Exhibition> GetExhibitionByIdAsync(int id)
        {
            return await _context.Exhibitions.FindAsync(id);
        }

        public async Task<IEnumerable<Exhibition>> SearchExhibitionsAsync(string query)
        {
            return await _context.Exhibitions
                .Where(e => e.Title.ToLower().Contains(query) || e.Description.ToLower().Contains(query))
                .ToListAsync();
        }

        public async Task AddExhibitionAsync(Exhibition exhibition)
        {
            await _context.Exhibitions.AddAsync(exhibition);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
