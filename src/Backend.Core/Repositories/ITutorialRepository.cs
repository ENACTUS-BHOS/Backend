namespace Backend.Core.Repositories
{
    using Backend.Core.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITutorialRepository
    {
        IEnumerable<Tutorial> GetAll();
        IEnumerable<string> GetAllCategories();
        int TutorialsCount(string? search, string? category, bool? isSortAscending);
        IEnumerable<Tutorial> Get(int skip, int take, string? search, string? category, bool? isSortAscending);
        Task<Tutorial?> GetByIdAsync(int id);
        IEnumerable<Tutorial> Get(int skip, int take);
        Task AddAsync(Tutorial tutorial);
        Task RemoveAsync(int id);
        Task UpdateAsync(int id, Tutorial newTutorial);
        IEnumerable<Tutorial> Search(string searchTerm); // Add search method
        IEnumerable<Tutorial> GetByCategory(string category); // Add method to get tutorials by category
    }
}
