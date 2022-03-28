using System;
using System.Collections.Generic;

namespace TTDesign.API.Domain.Models
{
    public partial class Leaveform
    {
        public long LeaveformId { get; set; }
        public long UserId { get; set; }
        public long LeaveTypeId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public float? Hours { get; set; }
        public string Status { get; set; } = null!;
        public string? Reason { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual LeaveType LeaveType { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
