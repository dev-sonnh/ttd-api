using TTDesign.API.Domain.Models;

namespace TTDesign.API.Resources
{
    public class SaveTeamUserResource
    {
        public long TeamUserId { get; set; }
        public long TeamId { get; set; }
        public long UserId { get; set; }
        public string? Role { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
