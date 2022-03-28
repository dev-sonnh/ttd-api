using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Repositories;
using TTDesign.API.Persistence.Contexts;

namespace TTDesign.API.Persistence.Repositories
{
    public class TimesheetCategoryRepository : BaseRepository, ITimesheetCategoryRepository
    {
        public TimesheetCategoryRepository(AppDbContext context) : base(context)
        {

        }

        public async Task CreateTimesheetCategory(TimesheetCategory timesheetCategory)
        {
            var parameters = FromModelTimesheetObjectToParams(timesheetCategory);
            await _context.Database.ExecuteSqlRawAsync("CALL usp_TimesheetCategories_InsertTimesheetCategory({0}, {1}, {2}, {3}, {4})", parameters);
        }

        public void DeleteTimesheetCategory(TimesheetCategory timesheetCategory)
        {
            _context.TimesheetCategories.Remove(timesheetCategory);
        }

        public async Task<IEnumerable<TimesheetCategory>> GetAllTimesheetCategories()
        {
            return await _context.TimesheetCategories.ToListAsync();
        }

        public async Task<IEnumerable<TimesheetCategory>> GetAllTimesheetCategoriesByTeamId(long id)
        {
            return await _context.TimesheetCategories.FromSqlRaw("CALL usp_TimesheetCategories_GetAllTimesheetCategoriesByTeamId({0})", id).ToListAsync();
        }

        public async Task<TimesheetCategory> GetTimesheetCategoryById(long id)
        {
            return await _context.TimesheetCategories.FindAsync(id);
        }

        public void UpdateTimesheetCategory(TimesheetCategory timesheetCategory)
        {
            _context.TimesheetCategories.Update(timesheetCategory);
        }

        public List<MySqlParameter> FromModelTimesheetObjectToParams(TimesheetCategory timesheetCategory)
        {
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(new MySqlParameter("name_parms", timesheetCategory.Name));
            parameters.Add(new MySqlParameter("team_id_parms", timesheetCategory.TeamId));
            parameters.Add(new MySqlParameter("is_active_parms", timesheetCategory.IsActive));
            parameters.Add(new MySqlParameter("is_public_parms", timesheetCategory.IsPublic));
            parameters.Add(new MySqlParameter("created_by_parms", timesheetCategory.CreatedBy));

            return parameters;
        }
    }
}
