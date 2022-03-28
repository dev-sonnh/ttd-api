using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Models.Extended;

namespace TTDesign.API.Domain.Services.Communication
{
    public class ReportsResponse : BaseResponse
    {
        public ReportSummaryTimesheetOfTeam ReportSummaryTimesheetOfTeam { get; private set; }

        private ReportsResponse(bool success, string message, ReportSummaryTimesheetOfTeam reportSummaryTimesheetOfTeam) : base(success, message)
        {
            ReportSummaryTimesheetOfTeam = reportSummaryTimesheetOfTeam;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="category">Saved category.</param>
        /// <returns>Response.</returns>
        public ReportsResponse(ReportSummaryTimesheetOfTeam reportSummaryTimesheetOfTeam) : this(true, string.Empty, reportSummaryTimesheetOfTeam)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public ReportsResponse(string message) : this(false, message, null)
        { }
    }
}
