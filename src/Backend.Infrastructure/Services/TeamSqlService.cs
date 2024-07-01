namespace Backend.Infrastructure.Services
{
    using Backend.Core.Models;
    using Backend.Core.Repositories;
    using Backend.Core.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class TeamSqlService : ITeamService
    {
        private readonly ITeamRepository teamRepository;

        public TeamSqlService(ITeamRepository teamRepository) => this.teamRepository = teamRepository;

        public IEnumerable<Team> GetAllTeams()
        {
            return this.teamRepository.GetAll();
        }

        public async Task AddAsync(Team? team)
        {
            ArgumentNullException.ThrowIfNull(team);
            await this.teamRepository.AddAsync(team);
        }

        public async Task RemoveAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(id);
            await this.teamRepository.RemoveAsync((int)id);
        }

        public async Task UpdateAsync(int? id, Team? newTeam)
        {
            ArgumentNullException.ThrowIfNull(id);
            ArgumentNullException.ThrowIfNull(newTeam);
            await this.teamRepository.UpdateAsync((int)id, newTeam);
        }
    }
}
