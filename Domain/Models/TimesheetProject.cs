﻿using System;
using System.Collections.Generic;

namespace TTDesign.API.Domain.Models
{
    public partial class TimesheetProject
    {
        public TimesheetProject()
        {
            Overtimes = new HashSet<Overtime>();
            TimesheetTasks = new HashSet<TimesheetTask>();
        }

        public long TimesheetProjectId { get; set; }
        public string? Code { get; set; }
        public string Name { get; set; } = null!;
        public long TeamId { get; set; }
        public DateTime StartedDate { get; set; }
        public DateTime? FinishedDate { get; set; }
        public ulong IsActive { get; set; }
        public ulong? IsPublic { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedBy { get; set; }

        public virtual Team Team { get; set; } = null!;
        public virtual ICollection<Overtime> Overtimes { get; set; }
        public virtual ICollection<TimesheetTask> TimesheetTasks { get; set; }
    }
}
