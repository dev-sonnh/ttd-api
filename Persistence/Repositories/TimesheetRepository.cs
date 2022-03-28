using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Repositories;
using TTDesign.API.Persistence.Contexts;

namespace TTDesign.API.Persistence.Repositories
{
    public class TimesheetRepository : BaseRepository, ITimesheetRepository
    {
        public TimesheetRepository(AppDbContext context) : base(context)
        {

        }

        public async Task CreateTimesheet(Timesheet timesheet)
        {
            var parameters = FromModelToParams(timesheet);
            await _context.Database.ExecuteSqlRawAsync("CALL usp_Timesheets_InsertTimesheet({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8} )", parameters);
        }

        public void DeleteTimesheet(Timesheet timesheet)
        {
            _context.Timesheets.Remove(timesheet);
        }

        public async Task<IEnumerable<Timesheet>> GetAllTimesheets()
        {
            return await _context.Timesheets.ToListAsync();
        }

        public async Task<Timesheet> GetTimesheetById(long id)
        {
            return await _context.Timesheets.FindAsync(id);
        }

        public void UpdateTimesheet(Timesheet timesheet)
        {
            _context.Timesheets.Update(timesheet);
        }

        //extended
        public async void UpdateTimesheetByFingerprintMachine(Timesheet timesheet)
        {
            var parameter = FromModelTimesheetToFingerPrintParams(timesheet);
            await _context.Database.ExecuteSqlRawAsync("CALL `ttdesigndatabase`.`usp_Timesheet_UpdateTimeByFingerprintMachine`({0}, {1}, {2}, {3})", parameter);
        }

        public async Task UpdateTimesheetByFingerprintMachineMultipleAsync(IEnumerable<Timesheet> timesheet)
        {
            foreach (var ts in timesheet)
            {
                var parameter = FromModelTimesheetToFingerPrintParams(ts);
                await _context.Database.ExecuteSqlRawAsync("CALL `ttdesigndatabase`.`usp_Timesheet_UpdateTimeByFingerprintMachine`({0}, {1}, {2}, {3})", parameter);
            }
        }

        //parameters
        public List<MySqlParameter> FromModelTimesheetToFingerPrintParams(Timesheet timesheet)
        {
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(new MySqlParameter("order_no_parms", timesheet.UserId));
            parameters.Add(new MySqlParameter("date_parms", timesheet.Date));
            parameters.Add(new MySqlParameter("time_in_parms", timesheet.TimeIn));
            parameters.Add(new MySqlParameter("time_out_parms", timesheet.TimeOut));

            return parameters;
        }

        public List<MySqlParameter> FromModelToParams(Timesheet model)
        {
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(new MySqlParameter("user_id", model.UserId));
            parameters.Add(new MySqlParameter("date", model.TimesheetId));
            parameters.Add(new MySqlParameter("time_in", model.TimeIn));
            parameters.Add(new MySqlParameter("time_out", model.TimeOut));
            parameters.Add(new MySqlParameter("note", model.Note));
            parameters.Add(new MySqlParameter("created_by", model.CreatedBy));

            return parameters;
        }

        public async Task<Timesheet> GetValidTimesheetByOrderNoAndDate(int orderNo, DateTime date)
        {
            return (await _context.Timesheets.FromSqlRaw("CALL usp_Timesheet_GetValidTimesheetByOrderNoAndDate({0}, {1})", orderNo, date).ToListAsync()).FirstOrDefault();
        }

        public async Task InsertTimeSheetMonthly()
        {
            await _context.Database.ExecuteSqlRawAsync("CALL usp_Timesheet_InsertTimesheetMonthly()");
        }
    }
}
