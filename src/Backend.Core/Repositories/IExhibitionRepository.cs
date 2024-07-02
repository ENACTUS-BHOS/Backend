namespace Backend.Core.Repositories
{
    using Backend.Core.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IExhibitionRepository
    {
        IEnumerable<Exhibition> GetAll();
        Task<Exhibition?> GetByIdAsync(int id);
        Task AddAsync(Exhibition exhibition);
        Task RemoveAsync(int id);
        Task UpdateAsync(int id, Exhibition newExhibition);
        IEnumerable<Exhibition> Search(string searchTerm); // Add search method
    }
}
