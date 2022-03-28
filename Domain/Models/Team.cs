using System;
using System.Collections.Generic;

namespace TTDesign.API.Domain.Models
{
    public partial class Team
    {
        public Team()
        {
            TeamUsers = new HashSet<TeamUser>();
            TimesheetCategories = new HashSet<TimesheetCategory>();
            TimesheetObjects = new HashSet<TimesheetObject>();
            TimesheetProjects = new HashSet<TimesheetProject>();
        }

        public long TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public string TeamCode { get; set; } = null!;
        public string? TeamDescription { get; set; }
        public ulong IsDepartment { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<TeamUser> TeamUsers { get; set; }
        public virtual ICollection<TimesheetCategory> TimesheetCategories { get; set; }
        public virtual ICollection<TimesheetObject> TimesheetObjects { get; set; }
        public virtual ICollection<TimesheetProject> TimesheetProjects { get; set; }
    }
}
