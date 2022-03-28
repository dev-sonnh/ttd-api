using System;
using System.Collections.Generic;

namespace TTDesign.API.Domain.Models
{
    public partial class Leave
    {
        public long LeaveId { get; set; }
        public long UserId { get; set; }
        public int Year { get; set; }
        public float AnnualLeaveDays { get; set; }
        public float AdditionalDays { get; set; }
        public ulong IsValid { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
