using System.IdentityModel.Tokens.Jwt;
using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Models.Extended;
using TTDesign.API.Domain.Repositories;
using TTDesign.API.Domain.Security.Hashing;
using TTDesign.API.Domain.Services;
using TTDesign.API.Domain.Services.Communication;
using TTDesign.API.Domain.Services.Communication.Extended;

namespace TTDesign.API.MySQL.Services
{
    public class ReportsService: IReportsService
    {
        private readonly IReportsRepository _reportsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReportsService(IReportsRepository reportsRepository, IUnitOfWork unitOfWork)
        {
            _reportsRepository = reportsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ReportSummaryTimesheetOfTeam>> GetReportSummaryTimesheetOfTeams(DateTime fromDate, DateTime toDate)
        {
            return await _reportsRepository.GetReportTimesheetSummaryOfTeam(fromDate, toDate);
        }

        public async Task<VwReportOfLeave> GetVwReportOfLeavesByUserId(long id)
        {
            return await _reportsRepository.GetVwReportOfLeavesByUserId(id);
        }
    }
}
