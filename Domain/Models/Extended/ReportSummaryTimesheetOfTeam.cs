namespace TTDesign.API.Domain.Models.Extended
{
    public class ReportSummaryTimesheetOfTeam
    {
        public long TeamId { get; set; }
        public string TeamCode { get; set; }
        public long TimesheetProjectId { get; set; }
        public string TimesheetProjectName { get; set; }
        public long UserId { get; set; }
        public string FullName { get; set; }
        public float Hours { get; set; }
    }
}
