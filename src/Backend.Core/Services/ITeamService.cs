namespace Backend.Core.Services;

using Backend.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITeamService
{
    IEnumerable<Team> GetAllTeams();
    Task AddAsync(Team? team);
    Task RemoveAsync(int? id);
    Task UpdateAsync(int? id, Team? newTeam);
}
