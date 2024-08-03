namespace Backend.Infrastructure.Services
{
    using Backend.Core.Models;
    using Backend.Core.Repositories;
    using Backend.Core.Services;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ExhibitionSqlService : IExhibitionService
    {
        private readonly IExhibitionRepository _exhibitionRepository;

        public ExhibitionSqlService(IExhibitionRepository exhibitionRepository)
        {
            _exhibitionRepository = exhibitionRepository;
        }

        public IEnumerable<Exhibition> GetAllExhibitions()
        {
            return _exhibitionRepository.GetAll();
        }

        public async Task<Exhibition?> GetByIdAsync(int id)
        {
            return await _exhibitionRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Exhibition>> GetAsync(int skip, int take, string? search)
        {
            return await this._exhibitionRepository.GetAsync(skip, take, search);
        }

        public async Task AddAsync(Exhibition exhibition)
        {
            await _exhibitionRepository.AddAsync(exhibition);
        }

        public async Task RemoveAsync(int id)
        {
            await _exhibitionRepository.RemoveAsync(id);
        }

        public async Task UpdateAsync(int id, Exhibition exhibition)
        {
            await _exhibitionRepository.UpdateAsync(id, exhibition);
        }

        public IEnumerable<Exhibition> Search(string searchTerm)
        {
            return _exhibitionRepository.Search(searchTerm);
        }
    }
}
