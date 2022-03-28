using System;
using System.Collections.Generic;

namespace TTDesign.API.Domain.Models
{
    public partial class TimesheetObject
    {
        public TimesheetObject()
        {
            TimesheetTasks = new HashSet<TimesheetTask>();
        }

        public long TimesheetObjectId { get; set; }
        public long TeamId { get; set; }
        public string Name { get; set; } = null!;
        public ulong IsActive { get; set; }
        public ulong IsPublic { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Team Team { get; set; } = null!;
        public virtual ICollection<TimesheetTask> TimesheetTasks { get; set; }
    }
}
