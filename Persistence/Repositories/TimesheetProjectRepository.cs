using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Repositories;
using TTDesign.API.Persistence.Contexts;

namespace TTDesign.API.Persistence.Repositories
{
    public class TimesheetProjectRepository : BaseRepository, ITimesheetProjectRepository
    {
        public TimesheetProjectRepository(AppDbContext context) : base(context)
        {

        }

        public async Task CreateTimesheetProject(TimesheetProject timesheetProject)
        {
            var parameters = FromModelTimesheetProjectToParams(timesheetProject);
            //await _context.TimesheetProjects.AddAsync(timesheetProject);
            await _context.Database.ExecuteSqlRawAsync("CALL usp_TimesheetProjects_InsertTimesheetProject({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})", parameters);
        }

        public void DeleteTimesheetProject(TimesheetProject timesheetProject)
        {
            _context.TimesheetProjects.Remove(timesheetProject);
        }

        public async Task<IEnumerable<TimesheetProject>> GetAllTimesheetProjects()
        {
            return await _context.TimesheetProjects.ToListAsync();
        }

        public async Task<IEnumerable<TimesheetProject>> GetAllTimesheetProjectsByTeamId(long id)
        {
            return await _context.TimesheetProjects.FromSqlRaw("CALL usp_TimesheetProjects_GetAllTimesheetProjectsByTeamId({0})", id).ToListAsync();
        }

        public async Task<TimesheetProject> GetTimesheetProjectById(long id)
        {
            return await _context.TimesheetProjects.FindAsync(id);
        }

        public void UpdateTimesheetProject(TimesheetProject timesheetProject)
        {
            _context.TimesheetProjects.Update(timesheetProject);
        }

        public List<MySqlParameter> FromModelTimesheetProjectToParams(TimesheetProject timesheetProject)
        {
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(new MySqlParameter("code_parms", timesheetProject.Code));
            parameters.Add(new MySqlParameter("name_parms", timesheetProject.Name));
            parameters.Add(new MySqlParameter("team_id_parms", timesheetProject.TeamId));
            parameters.Add(new MySqlParameter("started_date_parms", timesheetProject.StartedDate));
            parameters.Add(new MySqlParameter("finished_date_parms", timesheetProject.FinishedDate));
            parameters.Add(new MySqlParameter("is_active_parms", timesheetProject.IsActive));
            parameters.Add(new MySqlParameter("is_public_parms", timesheetProject.IsPublic));
            parameters.Add(new MySqlParameter("created_by_parms", timesheetProject.CreatedBy));

            return parameters;
        }
    }
}
