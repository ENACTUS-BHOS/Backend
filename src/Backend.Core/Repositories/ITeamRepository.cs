
namespace Backend.Core.Repositories
{
    using System.Collections.Generic;
    using Backend.Core.Models;
    using System.Threading.Tasks;

    public interface ITeamRepository
    {
        IEnumerable<Team> GetAll();
        Task<Team?> GetById(int id);
        Task AddAsync(Team team);
        Task RemoveAsync(int id);
        Task UpdateAsync(int id, Team newTeam);
    }
}
