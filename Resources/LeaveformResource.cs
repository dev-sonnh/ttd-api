using TTDesign.API.Domain.Models;

namespace TTDesign.API.Resources
{
    public class LeaveformResource
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

        public virtual User User { get; set; } = null!;
    }
}
