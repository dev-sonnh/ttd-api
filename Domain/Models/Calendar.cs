using System;
using System.Collections.Generic;

namespace TTDesign.API.Domain.Models
{
    public partial class Calendar
    {
        public Calendar()
        {
            CalendarAssignments = new HashSet<CalendarAssignment>();
        }

        public long CalendarId { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; } = null!;
        public ulong IsPublic { get; set; }
        public string? Color { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<CalendarAssignment> CalendarAssignments { get; set; }
    }
}
