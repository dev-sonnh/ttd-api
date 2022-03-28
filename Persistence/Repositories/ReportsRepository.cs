using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Data;
using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Models.Extended;
using TTDesign.API.Domain.Repositories;
using TTDesign.API.Persistence.Contexts;

namespace TTDesign.API.Persistence.Repositories
{
    public class ReportsRepository : BaseRepository, IReportsRepository
    {
        public ReportsRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<ReportSummaryTimesheetOfTeam>> GetReportTimesheetSummaryOfTeam(DateTime fromDate, DateTime toDate)
        {
            return await _context.Set<ReportSummaryTimesheetOfTeam>().FromSqlRaw("CALL usp_Report_Timesheet_SummaryOfTeamByDateRange({0}, {1})", fromDate, toDate).ToListAsync();
        }

        public async Task<VwReportOfLeave> GetVwReportOfLeavesByUserId(long id)
        {
            return (await _context.Set<VwReportOfLeave>().FromSqlRaw("CALL usp_Report_VwReportOfLeave_GetViewByUserId({0})", id).ToListAsync()).FirstOrDefault();
        }
    }
}
