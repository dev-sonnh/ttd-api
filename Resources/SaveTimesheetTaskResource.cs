using TTDesign.API.Domain.Models;

namespace TTDesign.API.Resources
{
    public class SaveTimesheetTaskResource
    {
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
    }
}
