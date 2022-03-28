using System;
using System.Collections.Generic;

namespace TTDesign.API.Domain.Models
{
    public partial class CalendarAssignment
    {
        public long CalendarAssignmentId { get; set; }
        public long CalendarId { get; set; }
        public long UserId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Calendar Calendar { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
