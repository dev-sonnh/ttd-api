using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Models.Extended;

namespace TTDesign.API.Domain.Repositories
{
    public interface IReportsRepository
    {
        Task<IEnumerable<ReportSummaryTimesheetOfTeam>> GetReportTimesheetSummaryOfTeam(DateTime fromDate, DateTime toDate);

        Task<VwReportOfLeave> GetVwReportOfLeavesByUserId(long id);
    }
}
