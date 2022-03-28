using System;
using System.Collections.Generic;

namespace TTDesign.API.Domain.Models
{
    public partial class Overtime
    {
        public long OvertimeId { get; set; }
        public long TimesheetId { get; set; }
        public long TimesheetProjectId { get; set; }
        public long UserId { get; set; }
        public TimeOnly? FromTime { get; set; }
        public TimeOnly? ToTime { get; set; }
        public string? Reason { get; set; }
        public float Hours { get; set; }
        public string Type { get; set; } = null!;
        public string Status { get; set; } = null!;
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Timesheet Timesheet { get; set; } = null!;
        public virtual TimesheetProject TimesheetProject { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
