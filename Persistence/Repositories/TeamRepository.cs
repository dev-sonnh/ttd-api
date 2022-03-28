using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Repositories;
using TTDesign.API.Persistence.Contexts;

namespace TTDesign.API.Persistence.Repositories
{
    public class TeamRepository : BaseRepository,ITeamRepository
    {
        public TeamRepository(AppDbContext context) : base(context)
        {

        }

        public async Task CreateTeam(Team team)
        {
            var parameter = FromModelToParamsInsert(team);
            await _context.Database.ExecuteSqlRawAsync("CALL usp_Teams_InsertTeam ({0} , {1}, {2}, {3}, {4})", parameter);
        }

        public void DeleteTeam(Team team)
        {
            _context.Teams.Remove(team);
        }

        public async Task<IEnumerable<Team>> GetAllTeams()
        {
            return await _context.Teams.ToListAsync();
        }

        public async Task<Team> GetTeamById(long id)
        {
            return await _context.Teams.FindAsync(id);
        }

        public void UpdateTeam(Team team)
        {
            _context.Teams.Update(team);
        }

        public List<MySqlParameter> FromModelToParamsInsert(Team model)
        {
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(new MySqlParameter("team_name_parms", model.TeamName));
            parameters.Add(new MySqlParameter("team_code_parms", model.TeamCode));
            parameters.Add(new MySqlParameter("team_description_parms", model.TeamDescription));
            parameters.Add(new MySqlParameter("is_department_parms", model.IsDepartment));
            parameters.Add(new MySqlParameter("created_by_parms", model.CreatedBy));

            return parameters;
        }

    }
}
