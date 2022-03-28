using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Models.Extended;
using TTDesign.API.Domain.Services.Communication;
using TTDesign.API.Domain.Services.Communication.Extended;

namespace TTDesign.API.Domain.Services
{
    public interface IReportsService
    {
        //common services
        Task<IEnumerable<ReportSummaryTimesheetOfTeam>> GetReportSummaryTimesheetOfTeams(DateTime fromDate, DateTime toDate);
        Task<VwReportOfLeave> GetVwReportOfLeavesByUserId(long id);

    }
}
