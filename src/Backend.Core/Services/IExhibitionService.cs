namespace Backend.Core.Services
{
    using Backend.Core.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IExhibitionService
    {
        IEnumerable<Exhibition> GetAllExhibitions();
        Task<Exhibition?> GetByIdAsync(int id);
        Task<IEnumerable<Exhibition>> GetAsync(int skip, int take, string? search);
        Task AddAsync(Exhibition exhibition);
        Task RemoveAsync(int id);
        Task UpdateAsync(int id, Exhibition exhibition);
        IEnumerable<Exhibition> Search(string searchTerm);
    }
}
