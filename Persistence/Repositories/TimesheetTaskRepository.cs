using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Repositories;
using TTDesign.API.Persistence.Contexts;

namespace TTDesign.API.Persistence.Repositories
{
    public class TimesheetTaskRepository : BaseRepository,ITimesheetTaskRepository
    {
        public TimesheetTaskRepository(AppDbContext context) : base(context)
        {

        }

        public async Task CreateTimesheetTask(TimesheetTask timesheetTask)
        {
            var parameters = FromModelToParams(timesheetTask);
            await _context.Database.ExecuteSqlRawAsync("CALL usp_TimesheetTasks_InsertTimesheetTask({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8} )", parameters);
        }

        public void DeleteTimesheetTask(TimesheetTask timesheetTask)
        {
            _context.TimesheetTasks.Remove(timesheetTask);
        }

        public async Task<IEnumerable<TimesheetTask>> GetAllTimesheetTasks()
        {
            return await _context.TimesheetTasks.ToListAsync();
        }

        public async Task<TimesheetTask> GetTimesheetTaskById(long id)
        {
            return await _context.TimesheetTasks.FindAsync(id);
        }

        public void UpdateTimesheetTask(TimesheetTask timesheetTask)
        {
            _context.TimesheetTasks.Update(timesheetTask);
        }

        public List<MySqlParameter> FromModelToParams(TimesheetTask model)
        {
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(new MySqlParameter("timesheet_id", model.TimesheetId));
            parameters.Add(new MySqlParameter("timesheet_project_id", model.TimesheetProjectId));
            parameters.Add(new MySqlParameter("timesheet_category_id", model.TimesheetCategoryId));
            parameters.Add(new MySqlParameter("timesheet_object_id", model.TimesheetObjectId));
            parameters.Add(new MySqlParameter("user_id", model.UserId));
            parameters.Add(new MySqlParameter("description", model.Description));
            parameters.Add(new MySqlParameter("hours", model.Hours));
            parameters.Add(new MySqlParameter("is_overtime", model.IsOvertime));
            parameters.Add(new MySqlParameter("created_by", model.CreatedBy));

            return parameters;
        }
    }
}
