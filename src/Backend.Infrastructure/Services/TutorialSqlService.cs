namespace Backend.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Backend.Core.Models;
    using Backend.Core.Repositories;
    using Backend.Core.Services;

    public class TutorialSqlService : ITutorialService
    {
        private readonly ITutorialRepository _tutorialRepository;

        public TutorialSqlService(ITutorialRepository tutorialRepository)
        {
            _tutorialRepository = tutorialRepository;
        }

        public IEnumerable<Tutorial> GetAllTutorials()
        {
            return _tutorialRepository.GetAll();
        }

        public async Task<Tutorial?> GetByIdAsync(int id)
        {
            return await _tutorialRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Tutorial tutorial)
        {
            if (tutorial == null)
            {
                throw new ArgumentNullException(nameof(tutorial));
            }

            await _tutorialRepository.AddAsync(tutorial);
        }

        public async Task RemoveAsync(int id)
        {
            await _tutorialRepository.RemoveAsync(id);
        }

        public async Task UpdateAsync(int id, Tutorial newTutorial)
        {
            if (newTutorial == null)
            {
                throw new ArgumentNullException(nameof(newTutorial));
            }

            await _tutorialRepository.UpdateAsync(id, newTutorial);
        }

        public IEnumerable<Tutorial> Search(string searchTerm)
        {
            return _tutorialRepository.Search(searchTerm);
        }

        public IEnumerable<Tutorial> GetByCategory(string category)
        {
            return _tutorialRepository.GetByCategory(category);
        }
    }
}
