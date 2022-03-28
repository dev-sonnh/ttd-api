using System;
using System.Collections.Generic;

namespace TTDesign.API.Domain.Models
{
    public partial class User
    {
        public User()
        {
            CalendarAssignments = new HashSet<CalendarAssignment>();
            Leaveforms = new HashSet<Leaveform>();
            Leaves = new HashSet<Leave>();
            Overtimes = new HashSet<Overtime>();
            TeamUsers = new HashSet<TeamUser>();
            TimesheetTasks = new HashSet<TimesheetTask>();
            Timesheets = new HashSet<Timesheet>();
            UserRoles = new HashSet<UserRole>();
        }

        public long UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int OrderNo { get; set; }
        public string? StaffId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public DateTime? Birthday { get; set; }
        public string? IdNo { get; set; }
        public string? IssuedTo { get; set; }
        public string? Address { get; set; }
        public string? AboutMe { get; set; }
        public string? Avatar { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual UserSetting UserSetting { get; set; } = null!;
        public virtual ICollection<CalendarAssignment> CalendarAssignments { get; set; }
        public virtual ICollection<Leaveform> Leaveforms { get; set; }
        public virtual ICollection<Leave> Leaves { get; set; }
        public virtual ICollection<Overtime> Overtimes { get; set; }
        public virtual ICollection<TeamUser> TeamUsers { get; set; }
        public virtual ICollection<TimesheetTask> TimesheetTasks { get; set; }
        public virtual ICollection<Timesheet> Timesheets { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
