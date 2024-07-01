namespace Backend.Infrastructure.Repositories
{
    using Backend.Core.Models;
    using Backend.Core.Repositories;
    using Backend.Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TeamSqlRepository : ITeamRepository
    {
        private readonly MirasDbContext dbContext;

        public TeamSqlRepository(MirasDbContext dbContext) => this.dbContext = dbContext;

        public IEnumerable<Team> GetAll()
        {
            var teams = this.dbContext.Teams.Where(t => t.IsActive == true);

            return teams;
        }

        public async Task<Team?> GetById(int id)
        {
            return await this.dbContext.Teams.AsNoTracking().FirstOrDefaultAsync(team => team.Id == id);
        }

        public async Task AddAsync(Team team)
        {
            await this.dbContext.Teams.AddAsync(team);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var team = await this.dbContext.Teams.FirstOrDefaultAsync(team => team.Id == id);
            if (team != null)
            {
                this.dbContext.Teams.Remove(team);
                await this.dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(int id, Team newTeam)
        {
            var team = await this.dbContext.Teams.FirstOrDefaultAsync(team => team.Id == id);
            
            if (team != null)
            {
                team.Name = newTeam.Name;
                team.Role = newTeam.Role;
                team.ImageUrl = newTeam.ImageUrl;
                team.Bio = newTeam.Bio;
                await this.dbContext.SaveChangesAsync();
            }
        }
    }
}
