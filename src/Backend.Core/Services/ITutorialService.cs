namespace Backend.Core.Services
{
    using Backend.Core.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    // public interface ITutorialService
    // {
    //     IEnumerable<Tutorial> GetAllTutorials();
    //     Task<Tutorial?> GetByIdAsync(int id);
    //     Task AddAsync(Tutorial? tutorial);
    //     Task RemoveAsync(int? id);
    //     Task UpdateAsync(int? id, Tutorial? newTutorial);
    //     IEnumerable<Tutorial> Search(string searchTerm); // Add search method
    // }
    public interface ITutorialService
    {
        IEnumerable<Tutorial> GetAllTutorials();
        Task<Tutorial?> GetByIdAsync(int id);
        Task AddAsync(Tutorial tutorial);
        Task RemoveAsync(int id);
        Task UpdateAsync(int id, Tutorial tutorial);
        IEnumerable<Tutorial> Search(string searchTerm);
    }
}
