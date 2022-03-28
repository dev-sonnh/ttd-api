using TTDesign.API.Domain.Models;

namespace TTDesign.API.Resources.Extended
{
    public class ReportOfLeaveAndLeaveformResource
    {
        public ReportOfLeaveAndLeaveformResource(ViewReportOfLeaveResource report, IEnumerable<LeaveformResource> leaveform)
        {
            ViewReportOfLeaveResource = report;
            ListLeaveforms = leaveform;
        }

        public ViewReportOfLeaveResource ViewReportOfLeaveResource { get; set; }

        public IEnumerable<LeaveformResource> ListLeaveforms { get; set; }
    }
}
