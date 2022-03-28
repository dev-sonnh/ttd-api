using System;
using System.Collections.Generic;

namespace TTDesign.API.Domain.Models
{
    public partial class LeaveType
    {
        public LeaveType()
        {
            Leaveforms = new HashSet<Leaveform>();
        }

        public long LeaveTypeId { get; set; }
        public string? Type { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<Leaveform> Leaveforms { get; set; }
    }
}
