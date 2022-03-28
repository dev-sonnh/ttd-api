using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Repositories;
using TTDesign.API.Persistence.Contexts;

namespace TTDesign.API.Persistence.Repositories
{
    public class TeamUserRepository : BaseRepository, ITeamUserRepository
    {
        public TeamUserRepository(AppDbContext context) : base(context)
        {

        }

        public async Task CreateTeamUser(TeamUser teamUser)
        {
            var parameters = FromModelToParams(teamUser);
            await _context.Database.ExecuteSqlRawAsync("CALL usp_TeamUser_InsertTeamUser({0}, {1}, {2}, {3}, {4} )", parameters);
        }

        public void DeleteTeamUser(TeamUser teamUser)
        {
            _context.TeamUsers.Remove(teamUser);
        }

        public async Task<IEnumerable<TeamUser>> GetAllTeamUsers()
        {
            return await _context.TeamUsers.ToListAsync();
        }

        public async Task<TeamUser> GetTeamUserById(long id)
        {
            return await _context.TeamUsers.FindAsync(id);
        }

        public void UpdateTeamUser(TeamUser teamUser)
        {
            _context.TeamUsers.Update(teamUser);
        }

        public List<MySqlParameter> FromModelToParams(TeamUser model)
        {
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(new MySqlParameter("team_id_parms", model.UserId));
            parameters.Add(new MySqlParameter("user_id_parms", model.UserId));
            parameters.Add(new MySqlParameter("role_parms", model.UserId));
            parameters.Add(new MySqlParameter("status_parms", model.UserId));
            parameters.Add(new MySqlParameter("created_by_parms", model.CreatedBy));

            return parameters;
        }

        public async void ChangeStatusById(long id, TeamUser teamUser)
        {
            await _context.Database.ExecuteSqlRawAsync("CALL usp_TeamUser_ChangeStatusById({0}, {1}, {2})", id, teamUser.Status, teamUser.ModifiedBy);
        }

        public async Task<TeamUser> GetTeamUserByUserIdAndTeamId(long userId, long teamId)
        {
            return (await _context.TeamUsers.FromSqlRaw("CALL usp_TeamUser_GetTeamUserByUserIdAndTeamId({0}, {1})", userId, teamId).ToListAsync()).FirstOrDefault();
        }
    }
}
