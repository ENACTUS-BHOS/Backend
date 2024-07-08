namespace Backend.Core.Services
{
    using Backend.Core.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITutorialService
    {
        IEnumerable<Tutorial> GetAllTutorials();
        Task<Tutorial?> GetByIdAsync(int id);
        IEnumerable<string> GetAllCategories();
        int TutorialsCount(string? search, string? category, bool? isSortAscending);
        IEnumerable<Tutorial> Get(int skip, int take, string? search, string? category, bool? isSortAscending);
        IEnumerable<Tutorial> Get(int skip, int take);
        Task AddAsync(Tutorial tutorial);
        Task RemoveAsync(int id);
        Task UpdateAsync(int id, Tutorial tutorial);
        IEnumerable<Tutorial> Search(string searchTerm);
        IEnumerable<Tutorial> GetByCategory(string category); // Add method to get tutorials by category
    }
}
    

