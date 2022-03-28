using System;
using System.Collections.Generic;

namespace TTDesign.API.Domain.Models
{
    public partial class TimesheetTask
    {
        public long TimesheetTaskId { get; set; }
        public long TimesheetId { get; set; }
        public long TimesheetProjectId { get; set; }
        public long TimesheetCategoryId { get; set; }
        public long TimesheetObjectId { get; set; }
        public long UserId { get; set; }
        public string? Description { get; set; }
        public float Hours { get; set; }
        public ulong IsOvertime { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Timesheet Timesheet { get; set; } = null!;
        public virtual TimesheetCategory TimesheetCategory { get; set; } = null!;
        public virtual TimesheetObject TimesheetObject { get; set; } = null!;
        public virtual TimesheetProject TimesheetProject { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
