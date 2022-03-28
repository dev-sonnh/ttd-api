using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Repositories;
using TTDesign.API.Persistence.Contexts;

namespace TTDesign.API.Persistence.Repositories
{
    public class TimesheetObjectRepository : BaseRepository, ITimesheetObjectRepository
    {
        public TimesheetObjectRepository(AppDbContext context) : base(context)
        {

        }

        public async Task CreateTimesheetObject(TimesheetObject timesheetObject)
        {
            //await _context.TimesheetObjects.AddAsync(timesheetObject);
            var parameters = FromModelTimesheetObjectToParams(timesheetObject);
            await _context.Database.ExecuteSqlRawAsync("CALL usp_TimesheetObjects_InsertTimesheetObject({0}, {1}, {2}, {3}, {4})", parameters);
        }

        public void DeleteTimesheetObject(TimesheetObject timesheetObject)
        {
            _context.TimesheetObjects.Remove(timesheetObject);
        }

        public async Task<IEnumerable<TimesheetObject>> GetAllTimesheetObjects()
        {
            return await _context.TimesheetObjects.ToListAsync();
        }

        public async Task<IEnumerable<TimesheetObject>> GetAllTimesheetObjectsByTeamId(long id)
        {
            return await _context.TimesheetObjects.FromSqlRaw("CALL usp_TimesheetObjects_GetAllTimesheetObjectsByTeamId({0})", id).ToListAsync();
        }

        public async Task<TimesheetObject> GetTimesheetObjectById(long id)
        {
            return await _context.TimesheetObjects.FindAsync(id);
        }

        public void UpdateTimesheetObject(TimesheetObject timesheetObject)
        {
            _context.TimesheetObjects.Update(timesheetObject);
        }

        public List<MySqlParameter> FromModelTimesheetObjectToParams(TimesheetObject timesheetObject)
        {
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(new MySqlParameter("team_id_parms", timesheetObject.TeamId));
            parameters.Add(new MySqlParameter("name_parms", timesheetObject.Name));
            parameters.Add(new MySqlParameter("is_active_parms", timesheetObject.IsActive));
            parameters.Add(new MySqlParameter("is_public_parms", timesheetObject.IsPublic));
            parameters.Add(new MySqlParameter("created_by_parms", timesheetObject.CreatedBy));

            return parameters;
        }
    }
}
