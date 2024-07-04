namespace Backend.Core.Repositories
{
    using Backend.Core.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITutorialRepository
    {
        IEnumerable<Tutorial> GetAll();
         Task<Tutorial?> GetByIdAsync(int id);
        Task AddAsync(Tutorial tutorial);
        Task RemoveAsync(int id);
        Task UpdateAsync(int id, Tutorial newTutorial);
        IEnumerable<Tutorial> Search(string searchTerm); // Add search method
        IEnumerable<Tutorial> GetByCategory(string category); // Add method to get tutorials by category
    }
}
