namespace TTDesign.API.Resources.Extended
{
    public class ReportSummaryTimesheetOfTeamResource
    {
        public List<Division> DivisionList { get; set; }

        
    }

    public class Division
    {
        public string DivisionName { get; set; }

        public List<Project> ProjectList { get; set; }
    }

    public class Project
    {
        public string ProjectName { get; set; }

        public List<Member> MemberList { get; set; }
    }

    public class Member
    {
        public long MemberId { get; set; }

        public string MemberName { get; set; }

        public float Hours { get; set; }
    }
}
