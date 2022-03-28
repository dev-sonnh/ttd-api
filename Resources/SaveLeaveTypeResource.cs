using TTDesign.API.Domain.Models;

namespace TTDesign.API.Resources
{
    public class SaveLeaveTypeResource
    {
        public string? Type { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
